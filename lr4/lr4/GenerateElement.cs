using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lr4
{
    delegate KeyValuePair<TKey, TValue> GenerateElement<TKey, TValue>(int j);
}
