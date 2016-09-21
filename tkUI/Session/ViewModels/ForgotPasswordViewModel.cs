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
    class ForgotPasswordViewModel : ObservableObject, IPageViewModelWithSizes
    {

        /* TODO: Keep implementing new features.
         * 1- EmailMessage
         * 2- Check if Email exists
         * 3- Color for the Message.
         * 4- Better formatting for this view.
         *  */


        #region Fields

        RelayCommand _requestPasswordReminderCommand;
        RelayCommand _loginCommand;
        Action<RequestedViewToGO> _changeViewModelManually;

        string _email;
        string _emailMessage;
        string _colorMessage;

        #endregion // Fields

        #region Constructors

        public ForgotPasswordViewModel(Action<RequestedViewToGO> changeViewModelManually)
        {
            _changeViewModelManually = changeViewModelManually;
        }

        #endregion // Constructors

        #region Properties

        public string Email
        {
            get { return _email; }
            set
            {
                if (_email == value)
                {
                    return;
                }

                _email = value;
                OnPropertyChanged("Email");
            }
        }

        // TODO: Here update the color message, error == red, Ok == green.
        public string EmailMessage
        {
            get { return _emailMessage; }
            set
            {
                if (_emailMessage == value)
                {
                    return;
                }

                _emailMessage = value;
                OnPropertyChanged("EmailMessage");
            }
        }

        public string ColorMessage
        {
            get { return _colorMessage; }
            set
            {
                if (_colorMessage == value)
                {
                    return;
                }

                _colorMessage = value;
                OnPropertyChanged("ColorMessage");
            }
        }

        #endregion // Properties

        #region Commands

        public ICommand RequestPasswordReminderCommand
        {
            get
            {
                if (_requestPasswordReminderCommand == null)
                {
                    _requestPasswordReminderCommand = new RelayCommand(
                        param => RequestReminder(),
                        param => CanRequestReminder()
                        );
                }

                return _requestPasswordReminderCommand;
            }
        }

        public ICommand LoginCommand
        {
            get
            {
                if (_loginCommand == null)
                {
                    _loginCommand = new RelayCommand(
                        param => GoToLogin(),
                        param => CanGoToLogin()
                        );
                }

                return _loginCommand;
            }
        }

        #endregion // Commands

        #region Private Helpers

        void RequestReminder()
        {
            throw new NotImplementedException("RequestReminder()");
        }

        bool CanRequestReminder()
        {
            if (Email != null && !RangeChecker.IsStringMissing(Email))
            {
                return true;
            }

            return false;
        }

        void GoToLogin()
        {
            _changeViewModelManually(RequestedViewToGO.LoginVM);
        }

        bool CanGoToLogin()
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
                return "430";
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
                return "530";
            }
            set { }
        }

        #endregion // Interface Implementations

    }
}
