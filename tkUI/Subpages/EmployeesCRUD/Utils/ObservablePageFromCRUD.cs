using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using tkUI.Helper_Classes;

namespace tkUI.Subpages.EmployeesCRUD.Utils
{
    /// <summary>
    /// Copies the functionality from PageFromCRUD class. Going to decide which to use later.
    /// </summary>
    abstract class ObservablePageFromCRUD : IButton, IRadioButtonChecked
    {

        #region Fields

        bool _isChecked;

        #endregion // Fields

        #region Interfaces

        public bool Checked
        {
            get { return _isChecked; }
            set { _isChecked = value; }
        }

        public abstract string Name { get; }

        #endregion // Interfaces
    }
}
