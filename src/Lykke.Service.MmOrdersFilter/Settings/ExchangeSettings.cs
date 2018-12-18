using JetBrains.Annotations;
using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.MmOrdersFilter.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class SourceExchangeSetting
    {
        [AmqpCheck]
        public string ConnectionString { set; get; }
        
        public string ExchangeName { set; get; }
        
        public string QueueSuffix { set; get; }
    }
    
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class DestinationExchangeSetting
    {
        [AmqpCheck]
        public string ConnectionString { set; get; }
        
        public string ExchangeName { set; get; }
    }
    
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class ExchangeMapping
    {
        public SourceExchangeSetting Source { set; get; }
        
        public DestinationExchangeSetting Destination { set; get; }
    }
}
