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
         * 1- EmailMessage OK
         * 2- Check if Email exists OK
         * 3- Color for the Message. OK
         * 4- Better formatting for this view.
         *  */


        #region Fields

        RelayCommand _requestPasswordReminderCommand;
        RelayCommand _loginCommand;
        Action<RequestedViewToGO> _changeViewModelManually;

        string _email;
        string _emailMessage;
        string _colorMessage = "White";

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

        /// <summary>
        /// Function that will send the email containing the password reminder instructions.
        /// It also does validation to check if the email is valid and it exists in the DB.
        /// </summary>
        void RequestReminder()
        {
            EmailMessage = "";

            if (!RangeChecker.IsValidEmailAddress(Email))
            {
                ColorMessage = "Red";
                EmailMessage = Resources.Employee_Error_InvalidEmail;
                return;
            }

            if (!LoginViewModel.EmailExists(Email))
            {
                ColorMessage = "Red";
                EmailMessage = Resources.LoginViewModel_Error_EmailWasntFound;
                return;
            }

            ColorMessage = "Green";
            EmailMessage = "¡Un email conteniendo el restableecimiento de tu clave fue enviado!";

            // TODO: Send the reminder here
            // TODO: We can use the redirection to the Login here too. After sending the reminder, we will
            // pass the message to the Login and go to that view.
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
            Email = "";
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
                return Resources.Session_ForgotPasswordViewModel_WindowTitle + Resources.App_Name;
            }
        }

        public string Height
        {
            get
            {
                return "300";
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
                return "400";
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
