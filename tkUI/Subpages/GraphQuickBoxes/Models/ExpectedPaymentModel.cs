using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using tkUI.Helper_Classes;
using tkUI.Subpages.GraphQuickBoxes.Utils;

namespace tkUI.Subpages.GraphQuickBoxes.Models
{
    class ExpectedPaymentModel
    {

        #region Fields

        List<double> _payments;
        List<Dates> _months;

        #endregion // Fields

        #region Properties
        
        public List<double> Payments
        {
            get { return _payments; }
            set { _payments = value; }
        }

        public List<Dates> Months
        {
            get { return _months; }
            set { _months = value; }
        }

        #endregion // Properties

        #region Constructors

        public ExpectedPaymentModel()
        {
            _payments = new List<double>();
            _months = new List<Dates>();
        }

        #endregion // Contructors


    }
}
