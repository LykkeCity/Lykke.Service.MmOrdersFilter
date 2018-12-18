using System;
using ProtoBuf;

namespace Lykke.Service.MmOrdersFilter.Contract.Trades
{
    /// <summary>
    /// Represents a trade that executed on Lykke exchange.
    /// </summary>
    [ProtoContract]
    public class Trade
    {
        [ProtoMember(1, IsRequired = true)]
        public string Id { get; set; }

        [ProtoMember(2, IsRequired = true)]
        public string LimitOrderId { get; set; }

        [ProtoMember(3, IsRequired = true)]
        public string ExchangeOrderId { get; set; }

        [ProtoMember(4, IsRequired = true)]
        public string AssetPairId { get; set; }

        [ProtoMember(5, IsRequired = true)]
        public TradeType Type { get; set; }

        [ProtoMember(6, IsRequired = true)]
        public DateTime Time { get; set; }

        [ProtoMember(7, IsRequired = true)]
        public decimal Price { get; set; }

        [ProtoMember(8, IsRequired = true)]
        public decimal Volume { get; set; }
        
        [ProtoMember(9, IsRequired = true)]
        public decimal RemainingVolume { get; set; }

        [ProtoMember(10, IsRequired = true)]
        public TradeStatus Status { get; set; }

        [ProtoMember(11, IsRequired = true)]
        public decimal OppositeSideVolume { get; set; }

        [ProtoMember(12, IsRequired = true)]
        public string WalletId { get; set; }
        
        [ProtoMember(13, IsRequired = true)]
        public string OppositeClientId { get; set; }

        [ProtoMember(14, IsRequired = true)]
        public string OppositeLimitOrderId { get; set; }

        [ProtoMember(14, IsRequired = true)]
        public int Index { get; set; }
    }
}
