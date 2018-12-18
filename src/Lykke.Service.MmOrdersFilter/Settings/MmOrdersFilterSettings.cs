using System.Collections.Generic;
using JetBrains.Annotations;

namespace Lykke.Service.MmOrdersFilter.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class MmOrdersFilterSettings
    {
        public DbSettings Db { get; set; }
        
        public string[] RelevantWalletIds { get; set; }
        
        public ExchangeMapping[] ExchangeMappings { set; get; }
    }
}
