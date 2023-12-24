using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UrashimaValidation.Functions
{
    public static class StringFunctions
    {
        public static bool MatchesRegexp(
            this string value,
            string pattern) 
        {
            return Regex.IsMatch(
                input: value,
                pattern: pattern);
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value: value);
        }

        public static bool IsEmpty(this string value)
        {
            return value == string.Empty;
        }

        public static bool IsEMail(this string value)
        {
            if (value == null)
            {
                return false;
            }

            return value.MatchesRegexp(pattern: @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$");
        }

        public static bool IsIPv4Address(this string value)
        {
            if (value == null)
            {
                return false;
            }

            return value.MatchesRegexp(pattern: @"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");
        }

        public static bool IsIPv6Address(this string value)
        {
            if (value == null)
            {
                return false;
            }

            return value.MatchesRegexp(pattern: @"(([0-9a-fA-F]{1,4}:){7,7}[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,7}:|([0-9a-fA-F]{1,4}:){1,6}:[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,5}(:[0-9a-fA-F]{1,4}){1,2}|([0-9a-fA-F]{1,4}:){1,4}(:[0-9a-fA-F]{1,4}){1,3}|([0-9a-fA-F]{1,4}:){1,3}(:[0-9a-fA-F]{1,4}){1,4}|([0-9a-fA-F]{1,4}:){1,2}(:[0-9a-fA-F]{1,4}){1,5}|[0-9a-fA-F]{1,4}:((:[0-9a-fA-F]{1,4}){1,6})|:((:[0-9a-fA-F]{1,4}){1,7}|:)|fe80:(:[0-9a-fA-F]{0,4}){0,4}%[0-9a-zA-Z]{1,}|::(ffff(:0{1,4}){0,1}:){0,1}((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])\.){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])|([0-9a-fA-F]{1,4}:){1,4}:((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])\.){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9]))");
        }

        public static bool IsGuid(this string value)
        {
            return Guid.TryParse(input: value, result: out Guid guid);
        }

        public static bool IsMatch(this string value, string pattern)
        {
            if (value == null)
            {
                return false;
            }

            return value.MatchesRegexp(pattern);
        }
    }
}
