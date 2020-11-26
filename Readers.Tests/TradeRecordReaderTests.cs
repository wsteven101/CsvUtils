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
    public class TradeRecordReaderTests
    {


        StringReader _strReader = new StringReader(
                                "TradeId,TradeType,Price\n" +
                                "1234567,BOND,102.5\n" +
                                "9876543,GILT,99.5");

        [Fact]
        public void test_read_non_existing_header_failure()
        {

            StringReader strFailReader = new StringReader(
                        "TradeId,Price\n" +
                        "1234567,BOND,102.5\n" +
                        "9876543,GILT,99.5");

            ICsvReader csvReader = new CsvReader(strFailReader);

            ITradeRecordReader tradeReader = new TradeRecordReader(csvReader);

            Assert.ThrowsAny<Exception>(() => tradeReader.ReadNextRecord());
        }

        [Fact]
        public void test_read_conversion_failure()
        {

            StringReader strFailReader = new StringReader(
                        "TradeId,TradeType,Price\n" +
                        "1234567,BOND,INVALID_DECIMAL\n" +
                        "9876543,GILT,99.5");

            ICsvReader csvReader = new CsvReader(strFailReader);

            ITradeRecordReader tradeReader = new TradeRecordReader(csvReader);

            Assert.ThrowsAny<AggregateException>(() => tradeReader.ReadNextRecord());
        }

        [Fact]
        public void test_read_csv_string_stream_success()
        {
            ICsvReader csvReader = new CsvReader(_strReader);

            ITradeRecordReader tradeReader = new TradeRecordReader(csvReader);

            TradeRecord rec1 = tradeReader.ReadNextRecord();

            Assert.Equal(1234567, rec1.TradeId);
            Assert.Equal("BOND", rec1.TradeType);
            Assert.Equal(102.5m, rec1.Price);

            TradeRecord rec2 = tradeReader.ReadNextRecord();

            Assert.Equal(9876543, rec2.TradeId);
            Assert.Equal("GILT", rec2.TradeType);
            Assert.Equal(99.5m, rec2.Price);

        }

        [Fact]
        public void test_IEnumerable_read_string_stream_success()
        {
            ICsvReader csvReader = new CsvReader(_strReader);

            ITradeRecordReader tradeReader = new TradeRecordReader(csvReader);

            var recs = new List<TradeRecord>();
            foreach (var rec in tradeReader.AsIEnumerable())
            {
                recs.Add(rec);
            }

            Assert.Equal(1234567, recs[0].TradeId);
            Assert.Equal("BOND", recs[0].TradeType);
            Assert.Equal(102.5m, recs[0].Price);

            Assert.Equal(9876543, recs[1].TradeId);
            Assert.Equal("GILT", recs[1].TradeType);
            Assert.Equal(99.5m, recs[1].Price);

        }

        [Fact]
        public void test_read_csv_file_success()
        {

            string fileName = "./testfile";

            File.WriteAllText(fileName, _strReader.ReadToEnd());

            var fileReader = new StreamReader(fileName);

            ICsvReader csvReader = new CsvReader(fileReader);

            ITradeRecordReader tradeReader = new TradeRecordReader(csvReader);

            var recs = new List<TradeRecord>();
            foreach (var rec in tradeReader.AsIEnumerable())
            {
                recs.Add(rec);
            }

            Assert.Equal(1234567, recs[0].TradeId);
            Assert.Equal("BOND", recs[0].TradeType);
            Assert.Equal(102.5m, recs[0].Price);

            Assert.Equal(9876543, recs[1].TradeId);
            Assert.Equal("GILT", recs[1].TradeType);
            Assert.Equal(99.5m, recs[1].Price);

        }

    }
}

