using JetBrains.Annotations;

namespace Lykke.Service.MmOrdersFilter.Client
{
    /// <summary>
    /// MmOrdersFilter client interface.
    /// </summary>
    [PublicAPI]
    public interface IMmOrdersFilterClient
    {
        // Make your app's controller interfaces visible by adding corresponding properties here.
        // NO actual methods should be placed here (these go to controller interfaces, for example - IMmOrdersFilterApi).
        // ONLY properties for accessing controller interfaces are allowed.

        /// <summary>Application Api interface</summary>
        IMmOrdersFilterApi Api { get; }
    }
}
