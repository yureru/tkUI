using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
