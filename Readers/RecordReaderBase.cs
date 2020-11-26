using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Readers.Interfaces;
using Readers.Types;

namespace Readers
{
    public abstract class RecordReaderBase<T> : IReader<T> where T : class
    {

            private readonly ICsvReader _reader;

            public RecordReaderBase(ICsvReader csvReader)
            {
                _reader = csvReader;
            }

            public T ReadNextRecord()
            {
                var rec = _reader.ReadNextRecord();

                if (rec == null)
                    return default;

                return ToRecord(rec);
            }

            public IEnumerable<T> AsIEnumerable()
            {
                var rec = _reader.ReadNextRecord();
                while (rec != default)
                {
                    yield return ToRecord(rec);
                    rec = _reader.ReadNextRecord();
                }
            }

            protected virtual T ToRecord(CsvRecord rec)
            {
                return default;
            }

        }
}
