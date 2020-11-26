using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

using Readers.Types;

namespace Readers.Tests
{
    public class CsvRecordTests
    {

        private Dictionary<string, string> _fieldData = new Dictionary<string, string>()
        {
            ["Col1"] = "TEST",
            ["Col2"] = "42",
            ["Col3"] = "180.2",
            ["Col4"] = "99.9"
        };

        [Fact]
        public void test_non_existant_header_column()
        {
            var rec = new CsvRecord(_fieldData);

            Assert.ThrowsAny<Exception>(() => rec.GetString("Col54321"));

        }

        [Fact]
        public void test_reading_fields_failure()
        {


            var rec = new CsvRecord(_fieldData);

             // conversion from string to a number should throw

            Assert.Throws<AggregateException>( () => rec.GetInteger("Col1") );
            Assert.Throws<AggregateException>( () => rec.GetDouble("Col1"));
            Assert.Throws<AggregateException>( () => rec.GetDecimal("Col1"));

        }


        [Fact]
        public void test_reading_fields_success()
        {
            var fieldData = new Dictionary<string, string>()
            {
                ["Col1"] = "TEST",
                ["Col2"] = "42",
                ["Col3"] = "180.2",
                ["Col4"] = "99.9"
            };

            var rec = new CsvRecord(fieldData);

            Assert.Equal(fieldData["Col1"], rec.GetString("Col1"));
            Assert.Equal(Int32.Parse(fieldData["Col2"]), rec.GetInteger("Col2"));
            Assert.Equal(Double.Parse(fieldData["Col3"]), rec.GetDouble("Col3"));
            Assert.Equal(Decimal.Parse(fieldData["Col4"]), rec.GetDecimal("Col4"));

        }
    }
}
