using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.MmOrdersFilter.Client 
{
    /// <summary>
    /// MmOrdersFilter client settings.
    /// </summary>
    public class MmOrdersFilterServiceClientSettings 
    {
        /// <summary>Service url.</summary>
        [HttpCheck("api/isalive")]
        public string ServiceUrl {get; set;}
    }
}
