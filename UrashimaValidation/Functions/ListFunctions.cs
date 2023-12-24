using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrashimaValidation.Functions
{
    public static class ListFunctions
    {
        public static bool In<T>(
            this T value,
            IEnumerable<T> values)
        {
            return values.Contains(value: value);
        }
    }
}
