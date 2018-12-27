using System;
using System.Threading.Tasks;
using Lykke.Common.Log;
using Lykke.RabbitMqBroker.Publisher;
using Lykke.RabbitMqBroker.Subscriber;
using Lykke.Service.MmOrdersFilter.Contract;
using Lykke.Service.MmOrdersFilter.Settings;
using Lykke.Service.MmOrdersFilter.Settings.ExchangeSettings;

namespace Lykke.Service.MmOrdersFilter.Rabbit
{
    public class Publisher<T> : IDisposable where T : IRoutable
    {
        private readonly DestinationExchangeSetting _exchangeSetting;
        private readonly ILogFactory _logFactory;
        private RabbitMqPublisher<T> _publisher;

        public Publisher(
            DestinationExchangeSetting exchangeSetting,
            ILogFactory logFactory)
        {
            _exchangeSetting = exchangeSetting;
            _logFactory = logFactory;
        }

        public Task Publish(T message)
        {
            return _publisher.ProduceAsync(message, message.GetRouteKey());
        }

        public void Start()
        {
            var settings = RabbitMqSubscriptionSettings
                .ForPublisher(_exchangeSetting.ConnectionString, _exchangeSetting.ExchangeName);

            _publisher = new RabbitMqPublisher<T>(_logFactory, settings)
                .SetSerializer(new ProtobufMessageSerializer<T>())
                .DisableInMemoryQueuePersistence()
                .SetPublishStrategy(new DirectPublishStrategy(settings))
                .Start();
        }

        public void Dispose()
        {
            _publisher?.Dispose();
        }

        public void Stop()
        {
            _publisher?.Stop();
        }
    }
}
