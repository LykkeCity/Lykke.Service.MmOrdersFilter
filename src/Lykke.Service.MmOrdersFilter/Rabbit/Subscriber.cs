using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lykke.Common.Log;
using Lykke.MatchingEngine.Connector.Models.Events;
using Lykke.RabbitMqBroker;
using Lykke.RabbitMqBroker.Subscriber;
using Lykke.Service.MmOrdersFilter.Contract;
using Lykke.Service.MmOrdersFilter.Contract.Trades;
using Lykke.Service.MmOrdersFilter.Settings;
using Trade = Lykke.Service.MmOrdersFilter.Contract.Trades.Trade;

namespace Lykke.Service.MmOrdersFilter.Rabbit
{
    public class Subscriber : IDisposable
    {
        private readonly ExchangeSetting _exchangeSettings;
        private readonly string _routingKey;
        private readonly string _queueSuffix;
        private readonly string[] _relevantWalletIds;
        private readonly Publisher<TradesMessage> _publisher;
        private RabbitMqSubscriber<ExecutionEvent> _subscriber;
        private readonly ILogFactory _logFactory;

        public Subscriber(
            ExchangeSetting exchangeSettings,
            string routingKey,
            string queueSuffix,
            string[] relevantWalletIds,
            Publisher<TradesMessage> publisher,
            ILogFactory logFactory)
        {
            _exchangeSettings = exchangeSettings;
            _routingKey = routingKey;
            _queueSuffix = queueSuffix;
            _relevantWalletIds = relevantWalletIds;
            _publisher = publisher;
            _logFactory = logFactory;
        }

        public void Start()
        {
            _publisher?.Start();

            RabbitMqSubscriptionSettings settings = RabbitMqSubscriptionSettings.ForSubscriber(
                    _exchangeSettings.ConnectionString,
                    _exchangeSettings.ExchangeName,
                    _queueSuffix)
                .UseRoutingKey(_routingKey);

            settings.MakeDurable();

            _subscriber = new RabbitMqSubscriber<ExecutionEvent>(
                    _logFactory,
                    settings,
                    new ResilientErrorHandlingStrategy(_logFactory, settings,
                        retryTimeout: TimeSpan.FromSeconds(10),
                        next: new DeadQueueErrorHandlingStrategy(_logFactory, settings)))
                .SetMessageDeserializer(new ProtobufMessageDeserializer<ExecutionEvent>())
                .SetMessageReadStrategy(new MessageReadQueueStrategy())
                .Subscribe(ProcessMessageAsync)
                .CreateDefaultBinding()
                .Start();
        }

        public void Stop()
        {
            _subscriber?.Stop();

            _publisher?.Stop();
        }

        public void Dispose()
        {
            _subscriber?.Dispose();

            _publisher?.Dispose();
        }

        private Task ProcessMessageAsync(ExecutionEvent arg)
        {
            var trades = CreateTrades(arg.Orders);

            foreach (var walletId in _relevantWalletIds)
            {
                var relevantTrades = trades.Where(x => x.WalletId == walletId).OrderBy(x => x.Id);

                if (relevantTrades.Any())
                {
                    _publisher.Publish(new TradesMessage
                    {
                        Trades = relevantTrades.ToList()
                    });
                }
            }

            return Task.CompletedTask;
        }

        private static IReadOnlyList<Trade> CreateTrades(IEnumerable<Order> orders)
        {
            var trades = new List<Trade>();

            foreach (Order order in orders)
            {
                // The limit order fully executed. The remaining volume is zero.
                if (order.Status == OrderStatus.Matched)
                {
                    trades.AddRange(CreateFillReports(order, true));
                }

                // The limit order partially executed.
                if (order.Status == OrderStatus.PartiallyMatched)
                {
                    trades.AddRange(CreateFillReports(order, false));
                }

                // The limit order was cancelled by matching engine after processing trades.
                // In this case order partially executed and remaining volume is less than min volume allowed by asset pair.
                if (order.Status == OrderStatus.Cancelled)
                {
                    trades.AddRange(CreateFillReports(order, true));
                }
            }

            return trades;
        }

        private static IReadOnlyList<Trade> CreateFillReports(Order order, bool completed)
        {
            var reports = new List<Trade>();

            for (int i = 0; i < order.Trades.Count; i++)
            {
                var trade = order.Trades[i];

                var tradeType = order.Side == OrderSide.Sell
                    ? TradeType.Sell
                    : TradeType.Buy;

                TradeStatus executionStatus = i == order.Trades.Count - 1 && completed
                    ? TradeStatus.Fill
                    : TradeStatus.PartialFill;

                var report = new Trade
                {
                    Id = Guid.NewGuid().ToString(),
                    AssetPairId = order.AssetPairId,
                    ExchangeOrderId = order.Id,
                    LimitOrderId = order.ExternalId,
                    Status = executionStatus,
                    Type = tradeType,
                    Time = trade.Timestamp,
                    Price = decimal.Parse(trade.Price),
                    Volume = Math.Abs(decimal.Parse(trade.BaseVolume)),
                    WalletId = order.WalletId,
                    OppositeClientId = trade.OppositeWalletId,
                    OppositeLimitOrderId = trade.OppositeOrderId,
                    OppositeSideVolume = Math.Abs(decimal.Parse(trade.QuotingVolume)),
                    RemainingVolume = decimal.Parse(order.RemainingVolume)
                };

                reports.Add(report);
            }

            return reports;
        }
    }
}
