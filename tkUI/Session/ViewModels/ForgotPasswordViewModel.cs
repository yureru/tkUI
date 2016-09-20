using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input;

using tkUI.Helper_Classes;

namespace tkUI.Session.ViewModels
{
    class ForgotPasswordViewModel : IPageViewModel
    {

        RelayCommand _requestRemainderCommand;

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
                return "¿Olvidaste tu contraseña? - timekeeping";
            }
        }

        #endregion // Interface Implementations

    }
}
