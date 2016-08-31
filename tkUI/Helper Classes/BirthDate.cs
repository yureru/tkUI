using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using tkUI.Properties;

namespace tkUI.Helper_Classes
{
    /// <summary>
    /// Class to retrieve a range of Days, Months, and Years that could be used in
    /// a Combobox tp select an specific date.
    /// </summary>
    class BirthDate
    {
        #region Fields

        static string[] _months;
        static List<string> _days;
        static List<string> _years;

        #endregion // Fields

        #region Properties

        public static List<string> Days
        {
            get
            {
                if (_days == null)
                {
                    _days = PopulateDaysOrYears(1, 31, true);
                }

                return _days; }
        }

        public static string[] Months
        {
            get
            {
                if (_months == null)
                {
                    PopulateMonths();
                }

                return _months;
            }
        }

        public static List<string> Years
        {
            get
            {
                if (_years == null)
                {
                    // minimumAge is the minimum allowed age to work
                    const int minimumAge = 15;
                    _years = PopulateDaysOrYears(DateTime.Today.Year - minimumAge, 1900, false);
                }

                return _years;
            }
        }

        #endregion // Properties

        #region Private Methods

        /// <summary>
        /// Populates the Days or the Years.
        /// </summary>
        static List<string> PopulateDaysOrYears(int from, int upTo, bool isPopulatingDays)
        {
            var items = new List<string>();
            // TODO: "Día" and "Año" should be string resources to allow use them in EmployeeWrapperViewModel
            if (isPopulatingDays)
            {
                items.Add(Resources.BirthDate_Combobox_Day);

                for (; from <= upTo; ++from)
                {
                    items.Add(from.ToString());
                }
            }
            else
            {
                items.Add(Resources.BirthDate_Combobox_Year);

                for (; from >= upTo; --from)
                {
                    items.Add(from.ToString());
                }
            }

            return items;
        }

        static void PopulateMonths()
        {
            _months = new string[]
                {
                    Resources.BirthDate_Combobox_Month,
                    "Enero", "Febrero", "Marzo", "Abril",
                    "Mayo", "Junio", "Julio", "Agosto",
                    "Septiembre", "Octubre", "Noviembre", "Diciembre"
                };
        }

        #endregion // Private Methods

        #region Methods

        /// <summary>
        /// Returns a number based on the month, where January is 1, February is 2, and December is 12.
        /// </summary>
        /// <param name="month">The name of the month.</param>
        /// <returns>The int representing a month, 0 if the month wasn't found.</returns>
        public static int ParseMonth(string month)
        {
            switch (month)
            {
                case "Enero":
                    return 1;
                case "Febrero":
                    return 2;
                case "Marzo":
                    return 3;
                case "Abril":
                    return 4;
                case "Mayo":
                    return 5;
                case "Junio":
                    return 6;
                case "Julio":
                    return 7;
                case "Agosto":
                    return 8;
                case "Septiembre":
                    return 9;
                case "Octubre":
                    return 10;
                case "Noviembre":
                    return 11;
                case "Diciembre":
                    return 12;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Validates a date with the values of Day, Month, and Year from the Comboboxes.
        /// </summary>
        /// <returns>An error message if the date is invalid or missing.</returns>
        public static string ValidateBirthdate(string day, string month, string year)
        {
            if (day != Resources.BirthDate_Combobox_Day
                && month != Resources.BirthDate_Combobox_Month
                && year != Resources.BirthDate_Combobox_Year)
            {
                try
                {
                    var date = new DateTime(int.Parse(year), BirthDate.ParseMonth(month), int.Parse(day));
                }
                catch (ArgumentOutOfRangeException)
                {
                    return Resources.EmployeeWrapperViewModel_Error_InvalidBirthDate;
                }

                return null;
            }

            return Resources.EmployeeWrapperViewModel_Error_MissingBirthDate;
        }

        #endregion // Methods

    }
}
