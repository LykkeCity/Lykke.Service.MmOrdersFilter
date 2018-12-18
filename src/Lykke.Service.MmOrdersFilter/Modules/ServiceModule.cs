using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Lykke.Common.Log;
using Lykke.MatchingEngine.Connector.Models.Events.Common;
using Lykke.Sdk;
using Lykke.Service.MmOrdersFilter.Contract.Trades;
using Lykke.Service.MmOrdersFilter.Managers;
using Lykke.Service.MmOrdersFilter.Rabbit;
using Lykke.Service.MmOrdersFilter.Settings;
using Lykke.SettingsReader;

namespace Lykke.Service.MmOrdersFilter.Modules
{
    public class ServiceModule : Module
    {
        private readonly IReloadingManager<AppSettings> _appSettings;

        public ServiceModule(IReloadingManager<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StartupManager>()
                .As<IStartupManager>();

            builder.RegisterType<ShutdownManager>()
                .As<IShutdownManager>();
            
            RegisterSubscribers(builder);
        }

        private void RegisterSubscribers(ContainerBuilder builder)
        {
            builder.Register(ctx =>
                {
                    var subscribers = new List<Subscriber>();

                    foreach (var mapping in _appSettings.CurrentValue.MmOrdersFilterService.ExchangeMappings)
                    {
                        var subscriber = new Subscriber(mapping.Source,
                            ((int) MessageType.Order).ToString(),
                            _appSettings.CurrentValue.MmOrdersFilterService.RelevantWalletIds,
                            new Publisher<TradesMessage>(mapping.Destination, ctx.Resolve<ILogFactory>()),
                            ctx.Resolve<ILogFactory>());

                        subscribers.Add(subscriber);
                    }

                    return subscribers;
                })
                .AsSelf()
                .SingleInstance();
        }
    }
}
