using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tkUI.Helper_Classes
{
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

        static List<string> PopulateDaysOrYears(int from, int upTo, bool isPopulatingDays)
        {
            var items = new List<string>();

            if (isPopulatingDays)
            {
                items.Add("Día");

                for (; from <= upTo; ++from)
                {
                    items.Add(from.ToString());
                }
            }
            else
            {
                items.Add("Año");

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
                    "Mes", "Enero", "Febrero", "Marzo",
                    "Abril", "Mayo", "Junio", "Julio",
                    "Agosto", "Septiembre", "Octubre", "Noviembre",
                    "Diciembre"
                };
        }

        #endregion // Private Methods

    }
}
