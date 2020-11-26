using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Readers.Interfaces;
using Readers.Types;

namespace Readers
{
    /// <summary>
    /// Class: TradeRecordReader
    /// This class takes an ICsvReader representing a csv stream 
    /// and returns instances of TradeRecord for each line in the cvs stream
    /// </summary>
    public class TradeRecordReader : RecordReaderBase<TradeRecord>, ITradeRecordReader
    {

        public TradeRecordReader(ICsvReader csvReader) : base(csvReader) { }

        protected override TradeRecord ToRecord(CsvRecord rec)
        {
            var tradeId = rec.GetInteger("TradeId");
            var tradeType = rec.GetString("TradeType");
            var price = rec.GetDecimal("Price");

            return new TradeRecord(tradeId, tradeType, price);
        }

    }
}
