using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrashimaValidation.Functions
{
    public static class RangeFunctions
    {
        public static bool Between(this double value, double start, double end)
        {
            return value >= start && value <= end;
        }

        public static bool Between(this float value, float start, float end)
        {
            return value >= start && value <= end;
        }

        public static bool Between(this int value, int start, int end)
        {
            return value >= start && value <= end;
        }

        public static bool Between(this DateTime value, DateTime start, DateTime end)
        {
            return value >= start && value <= end;
        }
    }
}
