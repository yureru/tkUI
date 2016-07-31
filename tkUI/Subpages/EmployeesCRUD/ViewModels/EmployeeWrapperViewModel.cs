using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Windows.Input;

using tkUI.Helper_Classes;
using tkUI.Models;

namespace tkUI.Subpages.EmployeesCRUD.ViewModels
{
    class EmployeeWrapperViewModel : ObservableObject, IDataErrorInfo
    {

        #region Fields

        readonly Employee _employee;
        

        #endregion // Fields

        #region Interface Implementations

        string IDataErrorInfo.Error
        {
            get
            {
                return null;
                //return (_employee as IDataErrorInfo).Error;
            }
        }

        // Used to validate an unselected Combobox.
        ///TODO: Validate Gender combobox here
        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                return null;
                //throw new NotImplementedException();
            }
        }

        #endregion // Interface Implementations

    }
}
