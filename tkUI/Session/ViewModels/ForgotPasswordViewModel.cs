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

        #region Fields

        RelayCommand _requestPasswordReminderCommand;
        RelayCommand _loginCommand;
        Action<RequestedViewToGO> _changeViewModelManually;
        Action<MessageAndColor> _setSuccessMessage;

        string _email;
        string _emailMessage;
        string _colorMessage = Resources.ForgotPasswordViewModel_Color_Blank;

        #endregion // Fields

        #region Constructors

        public ForgotPasswordViewModel(Action<RequestedViewToGO> changeViewModelManually, Action<MessageAndColor> setSuccessMessage)
        {
            _changeViewModelManually = changeViewModelManually;
            _setSuccessMessage = setSuccessMessage;
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
                ColorMessage = Resources.ForgotPasswordViewModel_Color_Error;
                EmailMessage = Resources.Employee_Error_InvalidEmail;
                return;
            }

            if (!LoginViewModel.EmailExists(Email))
            {
                ColorMessage = Resources.ForgotPasswordViewModel_Color_Error;
                EmailMessage = Resources.LoginViewModel_Error_EmailWasntFound;
                return;
            }

            var successMessage = new MessageAndColor()
            {
                    Color = Resources.ForgotPasswordViewModel_Color_Success,
                    Message = Resources.ForgotPasswordViewModel_Message_RecoveredPasswordMessage,
                    Email = this.Email
                };

            ColorMessage = Resources.ForgotPasswordViewModel_Color_Success;
            EmailMessage = Resources.ForgotPasswordViewModel_Message_RecoveredPasswordMessage;

            _setSuccessMessage(successMessage);

            // TODO: Send the reminder here

            GoToLogin();

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
