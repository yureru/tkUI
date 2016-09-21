using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input;

using tkUI.Helper_Classes;
using tkUI.Session.Utils;
using tkUI.Properties;

namespace tkUI.Session.ViewModels
{
    class ForgotPasswordViewModel : IPageViewModelWithSizes
    {

        #region Fields

        RelayCommand _requestRemainderCommand;
        Action<RequestedViewToGO> _changeViewModelManually;

        #endregion // Fields

        #region Constructors

        public ForgotPasswordViewModel(Action<RequestedViewToGO> changeViewModelManually)
        {
            _changeViewModelManually = changeViewModelManually;
        }

        #endregion // Constructors

        #region Commands

        public ICommand RequestRemainderCommand
        {
            get
            {
                if (_requestRemainderCommand == null)
                {
                    _requestRemainderCommand = new RelayCommand(
                        param => RequestRemainder(),
                        param => CanRequestRemainder()
                        );
                }

                return _requestRemainderCommand;
            }
        }

        #endregion // Commands

        #region Private Helpers

        void RequestRemainder()
        {
            throw new NotImplementedException("RequestRemainder()");
        }

        bool CanRequestRemainder()
        {
            return true;
        }

        #endregion // Private Helpers

        #region Interface Implementations

        public string Name
        {
            get
            {
                //return "¿Olvidaste tu contraseña? - timekeeping";
                return Resources.Session_ForgotPasswordViewModel_WindowTitle + Resources.App_Name;
            }
        }

        public string Height
        {
            get
            {
                return "200";
            }

            set { }
        }

        public string Width
        {
            get
            {
                return "150";
            }

            set { }
        }

        public string MinHeight
        {
            get
            {
                return Height;
            }
        }

        public string MinWidth
        {
            get
            {
                return Width;
            }
        }

        public string MaxHeight
        {
            get
            {
                return "300";
            }
            set { }
        }

        public string MaxWidth
        {
            get
            {
                return "250";
            }
            set { }
        }

        #endregion // Interface Implementations

    }
}
