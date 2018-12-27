using Lykke.HttpClientGenerator;

namespace Lykke.Service.MmOrdersFilter.Client
{
    /// <summary>
    /// MmOrdersFilter API aggregating interface.
    /// </summary>
    public class MmOrdersFilterClient : IMmOrdersFilterClient
    {
        // Note: Add similar Api properties for each new service controller

        /// <summary>Inerface to MmOrdersFilter Api.</summary>
        public IMmOrdersFilterApi Api { get; private set; }

        /// <summary>C-tor</summary>
        public MmOrdersFilterClient(IHttpClientGenerator httpClientGenerator)
        {
            Api = httpClientGenerator.Generate<IMmOrdersFilterApi>();
        }
    }
}
