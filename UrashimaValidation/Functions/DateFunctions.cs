using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrashimaValidation.Functions
{
    public static class DateFunctions
    {
        public static bool IsToday(this DateTime value)
        {
            DateTime today = DateTime.Today;
            return today.Year == value.Year &&
                today.Month == value.Month &&
                today.Day == value.Day;
        }
    }
}
