using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readers.Types
{
    /// <summary>
    /// Class: TradeRecord
    /// This class represents a record storing trade details
    /// </summary>
    public class TradeRecord
    {
        public int TradeId { get; init; }
        public string TradeType { get; init; }
        public Decimal Price { get; init; }

        public TradeRecord(int tradeId,
                            string tradeType,
                            decimal price)
        {
            TradeId = tradeId;
            TradeType = tradeType;
            Price = price;
        }
    }
}
