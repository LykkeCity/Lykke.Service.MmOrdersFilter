using JetBrains.Annotations;
using Lykke.Sdk.Settings;

namespace Lykke.Service.MmOrdersFilter.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AppSettings : BaseAppSettings
    {
        public MmOrdersFilterSettings MmOrdersFilterService { get; set; }
    }
}
