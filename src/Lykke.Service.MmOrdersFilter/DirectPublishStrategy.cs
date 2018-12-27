using System;
using Lykke.RabbitMqBroker.Publisher;
using Lykke.RabbitMqBroker.Subscriber;
using RabbitMQ.Client;

namespace Lykke.Service.MmOrdersFilter
{
    public sealed class DirectPublishStrategy : IRabbitMqPublishStrategy
    {
        private readonly bool _durable;
        private readonly string _routingKey;

        public DirectPublishStrategy(RabbitMqSubscriptionSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }
            _routingKey = settings.RoutingKey ?? string.Empty;
            _durable = settings.IsDurable;
        }

        public void Configure(RabbitMqSubscriptionSettings settings, IModel channel)
        {
            channel.ExchangeDeclare(exchange: settings.ExchangeName, type: "direct", durable: _durable);
        }

        public void Publish(RabbitMqSubscriptionSettings settings, IModel channel, RawMessage message)
        {
            channel.BasicPublish(exchange: settings.ExchangeName,
                routingKey: message.RoutingKey ?? _routingKey,
                basicProperties: null,
                body: message.Body);
        }
    }
}
