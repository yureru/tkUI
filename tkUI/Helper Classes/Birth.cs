using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tkUI.Helper_Classes
{
    class Birth
    {

        #region Fields

        string _day, _month, _year;

        #endregion // Fields

        #region Properties

        public string Day
        {
            get { return _day; }
            set { _day = value; }
        }

        public string Month
        {
            get { return _month; }
            set { _month = value; }
        }

        public string Year
        {
            get { return _year; }
            set { _year = value; }
        }

        public string Date
        {
            get { return String.Format("{0}/{1}/{2}", Day, Month, Year); }
        }

        #endregion // Properties


        #region Overloads

        #endregion // Overloads

        #region Methods

        public bool SetDate(string date)
        {
            var chunks = date.Split('/');

            if (chunks.Length != 3)
            {
                return false;
            }

            foreach (var data in chunks)
            {
                if (string.IsNullOrEmpty(data))
                {
                    return false;
                }
            }

            string d, m, y;
            d = chunks[0];
            m = chunks[1];
            y = chunks[2];

            var invalid = BirthDate.ValidateBirthdate(d, m, y);

            if (invalid != null)
            {
                return false;
            }

            Day = d;
            Month = m;
            Year = y;

            return true;
        }

        #endregion // Methods
    }
}
