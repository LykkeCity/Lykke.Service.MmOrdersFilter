using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Sdk;
using Lykke.Service.MmOrdersFilter.Rabbit;

namespace Lykke.Service.MmOrdersFilter.Managers
{
    public class ShutdownManager : IShutdownManager
    {
        private readonly List<Subscriber> _subscribers;
        
        public ShutdownManager(
            List<Subscriber> subscribers)
        {
            _subscribers = subscribers;
        }
        
        public Task StopAsync()
        {
            foreach (var subscriber in _subscribers)
            {
                subscriber.Stop();
            }

            return Task.CompletedTask;
        }
    }
}
