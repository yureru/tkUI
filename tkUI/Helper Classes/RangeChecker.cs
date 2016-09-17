using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Security;

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

        public static bool IsEqualTo(this SecureString sslh, SecureString ssrh)
        {
            IntPtr bstr1 = IntPtr.Zero;
            IntPtr bstr2 = IntPtr.Zero;

            try
            {
                bstr1 = Marshal.SecureStringToBSTR(sslh);
                bstr2 = Marshal.SecureStringToBSTR(ssrh);

                int length1 = Marshal.ReadInt32(bstr1, -4);
                int length2 = Marshal.ReadInt32(bstr2, -4);

                if (length1 == length2)
                {
                    for (int i = 0; i < length1; ++i)
                    {
                        byte b1 = Marshal.ReadByte(bstr1, i);
                        byte b2 = Marshal.ReadByte(bstr2, i);

                        if (b1 != b2)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
                return true;
            }
            finally
            {
                if (bstr1 != IntPtr.Zero)
                {
                    Marshal.ZeroFreeBSTR(bstr1);
                }

                if (bstr2 != IntPtr.Zero)
                {
                    Marshal.ZeroFreeBSTR(bstr2);
                }
            }
        }
    }
}
