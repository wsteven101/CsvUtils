using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Readers.Interfaces;
using Readers.Types;

namespace Readers
{

    /// <summary>
    /// Class: CsvReader
    /// This class takes a Stream such as a StringStream or FileStream and enables
    /// each line in the stream to beretrieved as an instance of a CsvRecord
    /// </summary>
    public class CsvReader : ICsvReader
    {

        private readonly TextReader _csvStream;
        private readonly char _delimiter;

        private bool _eos = false;
        private int _lineNo = 0;
        private string[] _fieldHeaders;

        public CsvReader(TextReader csvStream,
                         char demlimiter = ',')
        {
            _csvStream = csvStream;
            _delimiter = demlimiter;
        }

        public IEnumerable<CsvRecord> AsIEnumerable()
        {
            var rec = ReadNextRecord();
            while (rec != null)
            {
                yield return rec;
                rec = ReadNextRecord();
            }
        }

        public CsvRecord ReadNextRecord()
        {

            if (_eos)
            {
                return null;
            }

            try
            {
                if (_lineNo++ == 0)
                {
                    var headerLine = _csvStream.ReadLine();
                    if (headerLine == null)
                    {
                        _eos = true;
                        return null;
                    }
                    _fieldHeaders = headerLine.Split(_delimiter);
                }
                string line = _csvStream.ReadLine();

                if (line == null)
                {
                    _eos = true;
                    return null;
                }

                string[] fieldValues = line.Split(_delimiter);

                if (fieldValues.Length != _fieldHeaders.Length)
                {
                    throw new Exception($"Inconsistent data fields and headers at line {_lineNo}, contents:'{line}'");
                }

                Dictionary<string, string> fields = new Dictionary<string, string>();
                int idx = 0;
                foreach (var fieldValue in fieldValues)
                {
                    fields[_fieldHeaders[idx++]] = fieldValue;
                }
                return new CsvRecord(fields);

            }
            catch (Exception ex)
            {
                _csvStream.Dispose();
                throw new AggregateException("CSV reader failed to read stream", ex);
            }

        }

    }
}
