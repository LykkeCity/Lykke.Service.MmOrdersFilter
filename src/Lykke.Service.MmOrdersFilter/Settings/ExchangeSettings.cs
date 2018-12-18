using JetBrains.Annotations;
using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.MmOrdersFilter.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class ExchangeSetting
    {
        [AmqpCheck]
        public string ConnectionString { set; get; }
        
        public string ExchangeName { set; get; }
        
    }
    
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class ExchangeMapping
    {
        public ExchangeSetting Source { set; get; }
        
        public ExchangeSetting Destination { set; get; }
    }
}
