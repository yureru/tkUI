using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using tkUI.Helper_Classes;

namespace tkUI.Subpages.GraphQuickBoxes.Utils
{
    abstract class PageFromQuickBoxes : IBoxes, IRadioButtonChecked
    {

        #region Fields

        bool _isChecked;

        #endregion // Fields

        #region Interface Implementations

        // Interface IBoxes

        public abstract string BoxName { get; }

        public abstract string Quantity { get; set; }

        // Interface IRadioButtons

        public bool Checked
        {
            get
            {
                return _isChecked;
            }

            set
            {
                _isChecked = value;
            }
        }

        #endregion // Interface Implementations
    }
}
