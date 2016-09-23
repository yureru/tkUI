using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input;
using System.Security;
using tkUI.Helper_Classes;
using tkUI.Properties;

using tkUI.Session.Views;
using tkUI.Session.Utils;

namespace tkUI.Session.ViewModels
{

    class LoginViewModel : ObservableObject, IPageViewModelWithSizes
    {

        /* TODO: Implement message after registration.
         * For example, let's say we're bootstraping the app, the user will ned to register.
         * After doing it successfull, the view will need to change to be the Login one, and there
         * we can set a Message saying the registration was correct.
         */

        #region Fields

        RelayCommand _loginCommand;
        RelayCommand _forgotPasswordCommand;

        string _email;

        string _emailError;
        string _passError;

        MessageAndColor _successMessage;

        //Action<RequestedViewToGO, MessageAndColor> _changeViewModelManually;
        Action<RequestedViewToGO> _changeViewModelManually;

        #endregion // Fields

        #region Constructors

        public LoginViewModel(Action<RequestedViewToGO> changeViewModelManually)
        {
            _changeViewModelManually = changeViewModelManually;
            _successMessage = new MessageAndColor();
        }

        #endregion // Constructors

        #region Properties

        // TODO: DRY in ForgotPassword
        public string Email
        {
            get
            {
                return _email;
            }

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

        public string EmailError
        {
            get
            {
                return _emailError;
            }
            set
            {
                if (_emailError == value)
                {
                    return;
                }

                _emailError = value;

                OnPropertyChanged("EmailError");
            }
        }

        public string PassError
        {
            get
            {
                return _passError;
            }

            set
            {
                if (_passError == value)
                {
                    return;
                }

                _passError = value;

                OnPropertyChanged("PassError");
            }
        }

        // Wrappers to allow binding for MessageAndColor
        public string SuccessMessage
        {
            get
            {
                return _successMessage.Message;
            }

            set
            {
                if (_successMessage.Message == value)
                {
                    return;
                }

                _successMessage.Message = value;

                OnPropertyChanged("SuccessMessage");
            }
        }

        public string ColorSuccessMessage
        {
            get
            {
                return _successMessage.Color;
            }

            set
            {
                if (_successMessage.Color == value)
                {
                    return;
                }

                _successMessage.Color = value;

                OnPropertyChanged("ColorSuccessMessage");
            }
        }


        #endregion // Properties

        #region Commands

        public ICommand LoginCommand
        {
            get
            {
                if (_loginCommand == null)
                {
                    _loginCommand = new RelayCommand(
                        param => this.Login(),
                        param => this.CanLogin()
                        );
                }

                return _loginCommand;
            }
        }

        public ICommand ForgotPasswordCommand
        {
            get
            {
                if (_forgotPasswordCommand == null)
                {
                    _forgotPasswordCommand = new RelayCommand(
                        p => GoToForgotPassword(),
                        p => CanGoToForgotPassword()
                        );
                }

                return _forgotPasswordCommand;
            }
        }

        #endregion // Commands

        #region Private Helpers

        /// <summary>
        /// Function that *will* check user's entered credentials are correct.
        /// It needs to handle those in a secure way to avoid attacks.
        /// Currently using dummy values.
        /// </summary>
        void Login()
        {
            // TODO: Check if mail exists.

            if (!RangeChecker.IsValidEmailAddress(Email))
            {
                EmailError = Resources.Employee_Error_InvalidEmail;
                return;
            }

            if (!EmailExists(Email))
            {
                EmailError = Resources.LoginViewModel_Error_EmailWasntFound;
                return;
            }

            var passVisible = "test";

            SecureString pass;

            unsafe
            {
                fixed (char* p = passVisible)
                {
                    pass = new SecureString(p, 4);
                }

            }

            // TODO: Check if password matchs
            // TODO: Remove this to avoid high coupling
            if (!LoginView.Pass.IsEqualTo(pass))
            {
                EmailError = "";
                PassError = Resources.LoginViewModel_Error_WrongPassword;
                return;
            }

            tkUI.App.RunAppAfterSuccessfulLogin();
        }

        /// <summary>
        /// Checks if the Login button is available to the View by verifying fields aren't empty.
        /// </summary>
        /// <returns>True if can Login is available, false otherwise.</returns>
        bool CanLogin()
        {

            // Mail and Password fields are filled?
            if (Email != null && !RangeChecker.IsStringMissing(Email))
            {
                // TODO: remove this to avoid high coupling.
                if (LoginView.Pass != null && LoginView.Pass.Length != 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Go to the specified ViewModel.
        /// </summary>
        void GoToForgotPassword()
        {
            _changeViewModelManually(RequestedViewToGO.ForgotPasswordVM);
        }

        bool CanGoToForgotPassword()
        {
            return true;
        }

        #endregion // Private Helpers

        #region Methods

        /// <summary>
        /// Function that checks if given email exists in the DB.
        /// </summary>
        /// <param name="email">A valid email address.</param>
        /// <returns>True if email was found, false otherwise.</returns>
        public static bool EmailExists(string email)
        {
            // TODO: Add email checking here
            if (email == "test@gmail.com")
            {
                return true;
            }

            return false;
        }

        public void SetSuccessMessage(MessageAndColor msg)
        {
            ColorSuccessMessage = msg.Color;
            SuccessMessage = msg.Message;
        }

        #endregion

        #region Interface Implementations

        public string Name
        {
            get
            {
                return Resources.Session_LoginViewModel_WindowTitle + Resources.App_Name;
            }
        }

        public string Height
        {
            get
            {
                return "450";
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
                return "550";
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
