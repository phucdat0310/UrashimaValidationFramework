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
        /// <summary>
        /// Matches a regular expression
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <param name="pattern">The regular expression pattern</param>
        /// <exception cref="ArgumentNullException">Throws <see cref="ArgumentNullException"/> if the value or expression is null</exception>
        /// <returns>Returns true, if the value is valid. Else false</returns>
        public static bool MatchesRegexp(
            this string value,
            string pattern) 
        {
            return Regex.IsMatch(
                input: value,
                pattern: pattern);
        }

        /// <summary>
        /// Checks whether the string is null or empty
        /// </summary>
        /// <param name="value">The input value</param>
        /// <returns>Returns true if the input value is null or empty</returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value: value);
        }

        /// <summary>
        /// Checkts whether the string is empty
        /// </summary>
        /// <param name="value">The input value</param>
        /// <returns>Returns true if the input value is empty</returns>
        public static bool IsEmpty(this string value)
        {
            return value == string.Empty;
        }

        /// <summary>
        /// Checks whether the string represents a email address
        /// </summary>
        /// <param name="value">The input value</param>
        /// <returns>Returns true if the input value represents a email address otherwise false</returns>
        public static bool IsEMail(this string value)
        {
            if (value == null)
            {
                return false;
            }

            return value.MatchesRegexp(pattern: @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$");
        }

        /// <summary>
        /// Checks whether the string represents a IP-V4-Address
        /// </summary>
        /// <param name="value">The input value</param>
        /// <returns>Returns true if the input value represents a IP-V4-Address otherwise false</returns>
        public static bool IsIPv4Address(this string value)
        {
            if (value == null)
            {
                return false;
            }

            return value.MatchesRegexp(pattern: @"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");
        }

        /// <summary>
        /// Checks whether the string represents a IP-V6-Address
        /// </summary>
        /// <param name="value">The input value</param>
        /// <returns>Returns true if the input value represents a IP-V6-Address otherwise false</returns>
        public static bool IsIPv6Address(this string value)
        {
            if (value == null)
            {
                return false;
            }

            return value.MatchesRegexp(pattern: @"(([0-9a-fA-F]{1,4}:){7,7}[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,7}:|([0-9a-fA-F]{1,4}:){1,6}:[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,5}(:[0-9a-fA-F]{1,4}){1,2}|([0-9a-fA-F]{1,4}:){1,4}(:[0-9a-fA-F]{1,4}){1,3}|([0-9a-fA-F]{1,4}:){1,3}(:[0-9a-fA-F]{1,4}){1,4}|([0-9a-fA-F]{1,4}:){1,2}(:[0-9a-fA-F]{1,4}){1,5}|[0-9a-fA-F]{1,4}:((:[0-9a-fA-F]{1,4}){1,6})|:((:[0-9a-fA-F]{1,4}){1,7}|:)|fe80:(:[0-9a-fA-F]{0,4}){0,4}%[0-9a-zA-Z]{1,}|::(ffff(:0{1,4}){0,1}:){0,1}((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])\.){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])|([0-9a-fA-F]{1,4}:){1,4}:((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])\.){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9]))");
        }

        /// <summary>
        /// checks the value if it is a Guid
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <returns>True if the value represents a Guid. Else false.</returns>
        public static bool IsGuid(this string value)
        {
            return Guid.TryParse(input: value, result: out Guid guid);
        }
    }
}
