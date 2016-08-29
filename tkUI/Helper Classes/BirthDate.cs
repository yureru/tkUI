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

    }
}
