using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Readers.Types;

namespace Readers.Interfaces
{
    public interface IReader<T> where T : class
    {
        public T ReadNextRecord();
        public IEnumerable<T> AsIEnumerable();
    }
}
