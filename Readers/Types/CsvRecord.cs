using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readers.Types
{

    /// <summary>
    /// Class: CsvRecord
    /// This class represents and stores a line within a csv file as internal fields.
    /// For example GetInteger('Quantity') to access the field 'Quantity' as an integer
    /// </summary>

    public class CsvRecord
    {
        private readonly IDictionary<string, string> _fields;

        public CsvRecord(IDictionary<string, string> fields) => _fields = fields;
        private T convertData<T>(string fieldName, Func<string, T> valueConverter)
        {
            string dataStr;
            if (!_fields.TryGetValue(fieldName, out dataStr))
            {
                throw new Exception($"The requested field {fieldName.ToString()} does not exist in the csv file  heading");
            }

            try
            {
                return valueConverter(dataStr);
            }
            catch (Exception ex)
            {
                throw new AggregateException($"The field {fieldName} with data '{dataStr}'" +
                                             $"could not be converted to a {typeof(T)}", ex);
            }
        }

        public string GetString(string fieldName) => convertData(fieldName, (s) => s);
        public int GetInteger(string fieldName) => convertData(fieldName, (s) => Int32.Parse(s));
        public double GetDouble(string fieldName) => convertData(fieldName, (s) => Double.Parse(s));
        public decimal GetDecimal(string fieldName) => convertData(fieldName, (s) => Decimal.Parse(s));

    }

}
