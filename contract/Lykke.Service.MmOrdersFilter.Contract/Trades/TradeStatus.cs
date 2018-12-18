using ProtoBuf;

namespace Lykke.Service.MmOrdersFilter.Contract.Trades
{
    /// <summary>
    /// Specifies trade status.
    /// </summary>
    public enum TradeStatus
    {
        /// <summary>
        /// Unspecified trade status.
        /// </summary>
        None,

        /// <summary>
        /// Fully executed limit order.
        /// </summary>
        Fill,

        /// <summary>
        /// Partially executed limit order.
        /// </summary>
        PartialFill
    }
}
