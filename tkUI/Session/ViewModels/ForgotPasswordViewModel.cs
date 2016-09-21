using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input;

using tkUI.Helper_Classes;
using tkUI.Session.Utils;

namespace tkUI.Session.ViewModels
{
    class ForgotPasswordViewModel : IPageViewModel, IWindowViewSizes
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
                return "¿Olvidaste tu contraseña? - timekeeping";
            }
        }

        public string Height
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Width
        {
            get
            {
                throw new NotImplementedException();
            }

        }

        public string MinHeight
        {
            get
            {
                throw new NotImplementedException();
            }

        }

        public string MinWidth
        {
            get
            {
                throw new NotImplementedException();
            }

        }

        public string MaxHeight
        {
            get
            {
                throw new NotImplementedException();
            }

        }

        public string MaxWidth
        {
            get
            {
                throw new NotImplementedException();
            }

        }

        #endregion // Interface Implementations

    }
}
