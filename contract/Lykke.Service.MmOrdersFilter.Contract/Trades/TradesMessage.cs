using System.Collections.Generic;
using System.Linq;
using ProtoBuf;

namespace Lykke.Service.MmOrdersFilter.Contract.Trades
{
    [ProtoContract]
    public class TradesMessage : IRoutable
    {
        [ProtoMember(1, IsRequired = true)]
        public List<Trade> Trades { set; get; }
        
        public string GetRouteKey()
        {
            return Trades.First().WalletId;
        }
    }
}
