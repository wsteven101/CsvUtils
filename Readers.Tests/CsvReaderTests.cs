using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

using Readers.Types;
using Readers.Interfaces;
using System.IO;

namespace Readers.Tests
{
    public class CsvReaderTests
    {

        StringReader _strReader = new StringReader(
                                        "TradeId,TradeType,Price\n" +
                                        "1234567,BOND,102.5\n" +
                                        "9876543,GILT,99.5");

        [Fact]
        public void test_inconsistent_fields_in_2nd_line_failure()
        {
            StringReader strFailReader = new StringReader(
                        "TradeId,TradeType,Price\n" +
                        "1234567,BOND,102.5\n" +
                        "9876543,GILT,99.5,EXTRA_FIELD");

            ICsvReader csvReader = new CsvReader(strFailReader);

             // skip 1st line
            _ = csvReader.ReadNextRecord();

            Assert.ThrowsAny<Exception>( () => csvReader.ReadNextRecord() );
        }

        [Fact]
        public void test_read_csv_string_stream_success()
        {
            ICsvReader csvReader = new CsvReader(_strReader);

            var rec1 = csvReader.ReadNextRecord();

            Assert.Equal(1234567,rec1.GetInteger("TradeId"));
            Assert.Equal("BOND", rec1.GetString("TradeType"));
            Assert.Equal(102.5m, rec1.GetDecimal("Price"));

            var rec2 = csvReader.ReadNextRecord();

            Assert.Equal(9876543, rec2.GetInteger("TradeId"));
            Assert.Equal("GILT", rec2.GetString("TradeType"));
            Assert.Equal(99.5m, rec2.GetDecimal("Price"));

        }

        [Fact]
        public void test_IEnumerable_read_string_stream_success()
        {
            ICsvReader csvReader = new CsvReader(_strReader);

            var recs = new List<CsvRecord>();
            foreach (var rec in csvReader.AsIEnumerable())
            {
                recs.Add(rec);
            }

            var rec1 = recs[0];

            Assert.Equal(1234567, rec1.GetInteger("TradeId"));
            Assert.Equal("BOND", rec1.GetString("TradeType"));
            Assert.Equal(102.5m, rec1.GetDecimal("Price"));

            var rec2 = recs[1];

            Assert.Equal(9876543, rec2.GetInteger("TradeId"));
            Assert.Equal("GILT", rec2.GetString("TradeType"));
            Assert.Equal(99.5m, rec2.GetDecimal("Price"));

        }

    }
}
