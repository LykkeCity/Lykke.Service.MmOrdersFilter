using System.Collections.Generic;
using Autofac;
using Common;
using Lykke.Service.MmOrdersFilter.Contract.Trades;

namespace Lykke.Service.MmOrdersFilter.Rabbit
{
    public class EventsSubscriber : IStartable, IStopable
    {
        private readonly List<Publisher<TradesMessage>> _tradesPublishers;

        public EventsSubscriber()
        {
            _tradesPublishers = new List<Publisher<TradesMessage>>();
        }
        
        public void Start()
        {
            
            
            _tradesPublishers.ForEach(x => x?.Start());
            
            
        }

        public void Dispose()
        {
            _tradesPublishers.ForEach(x => x?.Dispose());
        }

        public void Stop()
        {
            _tradesPublishers.ForEach(x => x?.Stop());
        }
    }
}
