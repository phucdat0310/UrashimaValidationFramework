using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrashimaValidation.Functions
{
    /// <summary>
    /// Range-Functions
    /// </summary>
    public static class RangeFunctions
    {
        /// <summary>
        /// Is the value between start and end
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="start">The start value</param>
        /// <param name="end">The end value</param>
        /// <returns>True if the value is between start and end. Else false.</returns>
        public static bool Between(this double value, double start, double end)
        {
            return value >= start && value <= end;
        }

        /// <summary>
        /// Is the value between start and end
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="start">The start value</param>
        /// <param name="end">The end value</param>
        /// <returns>True if the value is between start and end. Else false.</returns>
        public static bool Between(this float value, float start, float end)
        {
            return value >= start && value <= end;
        }

        /// <summary>
        /// Is the value between start and end
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="start">The start value</param>
        /// <param name="end">The end value</param>
        /// <returns>True if the value is between start and end. Else false.</returns>
        public static bool Between(this int value, int start, int end)
        {
            return value >= start && value <= end;
        }

        /// <summary>
        /// Is the value between start and end
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="start">The start value</param>
        /// <param name="end">The end value</param>
        /// <returns>True if the value is between start and end. Else false.</returns>
        public static bool Between(this DateTime value, DateTime start, DateTime end)
        {
            return value >= start && value <= end;
        }
    }
}
