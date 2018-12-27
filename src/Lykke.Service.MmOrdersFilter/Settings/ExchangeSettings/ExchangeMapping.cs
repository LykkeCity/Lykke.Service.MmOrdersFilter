using JetBrains.Annotations;

namespace Lykke.Service.MmOrdersFilter.Settings.ExchangeSettings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class ExchangeMapping
    {
        public SourceExchangeSetting Source { set; get; }
	
        public DestinationExchangeSetting Destination { set; get; }
    }
}
