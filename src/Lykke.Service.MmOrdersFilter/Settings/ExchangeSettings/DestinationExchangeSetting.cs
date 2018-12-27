using JetBrains.Annotations;
using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.MmOrdersFilter.Settings.ExchangeSettings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class DestinationExchangeSetting
    {
        [AmqpCheck]
        public string ConnectionString { set; get; }
	
        public string ExchangeName { set; get; }
    }
}
