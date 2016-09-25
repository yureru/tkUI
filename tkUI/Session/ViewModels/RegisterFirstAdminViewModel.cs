using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using tkUI.Properties;
using tkUI.Session.Utils;
using tkUI.Subpages.EmployeesCRUD.ViewModels;


namespace tkUI.Session.ViewModels
{
    class RegisterFirstAdminViewModel :  IPageViewModelWithSizes
    {

        #region Fields

        Action<RequestedViewToGO> _changeViewManually;
        Action<MessageAndColor> _setSuccessMessage;

        #endregion // Fields

        #region Constructors

        public RegisterFirstAdminViewModel(Action<RequestedViewToGO> changeViewManually, Action<MessageAndColor> setSuccessMessage)
        {
            _changeViewManually = changeViewManually;
            _setSuccessMessage = setSuccessMessage;
        }

        #endregion // Constructors

        #region Properties
        #endregion // Properties


        #region Private Helpers
        #endregion // Private Helpers

        #region Interface Implementations

        public string Name
        {
            get
            {
                return Resources.Session_RegisterViewModel_WindowTitle + Resources.App_Name;
            }
        }

        public string Height
        {
            get
            {
                return "600";
            }

            set { }
        }

        public string Width
        {
            get
            {
                return "500";
            }

            set { }
        }

        public string MinHeight
        {
            get
            {
                return Height;
            }

            set { }
        }

        public string MinWidth
        {
            get
            {
                return Width;
            }

            set { }
        }

        public string MaxHeight
        {
            get
            {
                return "800";
            }
            set { }
        }

        public string MaxWidth
        {
            get
            {
                return "600";
            }
            set { }
        }

        #endregion // Interface Implementations
    }
}
