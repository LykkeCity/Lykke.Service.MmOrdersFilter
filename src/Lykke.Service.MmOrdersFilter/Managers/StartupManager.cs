using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Sdk;
using Lykke.Service.MmOrdersFilter.Rabbit;

namespace Lykke.Service.MmOrdersFilter.Managers
{
    public class StartupManager : IStartupManager
    {
        private readonly List<Subscriber> _subscribers;
        
        public StartupManager(
            List<Subscriber> subscribers)
        {
            _subscribers = subscribers;
        }
        
        public Task StartAsync()
        {
            foreach (var subscriber in _subscribers)
            {
                subscriber.Start();
            }

            return Task.CompletedTask;
        }
    }
}
