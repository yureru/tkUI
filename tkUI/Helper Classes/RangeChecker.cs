using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;

namespace tkUI.Helper_Classes
{
    /// <summary>
    /// Serves to group utilities functions to check if a value is within the expected range.
    /// </summary>
    static class RangeChecker
    {
        /// <summary>
        /// Checks if given string contains only digits.
        /// </summary>
        /// <returns>True if contains only digits, false otherwise.</returns>
        public static bool IsDigitsOnly(string Number)
        {
            foreach (var c in Number)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }

        public static bool IsValidEmailAddress(string email)
        {
            if (IsStringMissing(email))
                return false;

            // This regex pattern came from: http://haacked.com/archive/2007/08/21/i-knew-how-to-validate-an-email-address-until-i.aspx
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }

        public static bool IsStringMissing(string value)
        {
            return String.IsNullOrEmpty(value) ||
                value.Trim() == String.Empty;
        }

    }
}
