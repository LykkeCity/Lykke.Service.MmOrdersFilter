namespace Lykke.Service.MmOrdersFilter.Contract.Trades
{
    /// <summary>
    /// Specifies a type of trade.
    /// </summary>
    public enum TradeType
    {
        /// <summary>
        /// Unspecified type.
        /// </summary>
        None,

        /// <summary>
        /// Executed buy limit order.
        /// </summary>
        Buy,

        /// <summary>
        /// Executed sell limit order.
        /// </summary>
        Sell
    }
}
