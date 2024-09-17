using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lr5
{
    delegate KeyValuePair<TKey, TValue> GenerateElement<TKey, TValue>(int j);
}
