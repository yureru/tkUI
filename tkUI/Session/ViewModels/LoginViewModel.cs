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

        #region Fields

        RelayCommand _loginCommand;
        RelayCommand _forgotPasswordCommand;

        string _email;

        string _emailError;
        string _passError;

        Action<RequestedViewToGO> _changeViewModelManually;

        #endregion // Fields

        #region Constructors

        public LoginViewModel(Action<RequestedViewToGO> changeViewModelManually)
        {
            _changeViewModelManually = changeViewModelManually;
        }

        #endregion // Constructors

        #region Properties

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

            if (Email != "test@gmail.com")
            {
                EmailError = Resources.LoginViewModel_Error_EmailWasntFound;
                return;
            }
            /*var passUnsecure = new StringBuilder("test");
            var pass = new SecureString(passUnsecure, 4);*/
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
                PassError = Properties.Resources.LoginViewModel_Error_WrongPassword;
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

        void GoToForgotPassword()
        {
            //throw new NotImplementedException("GoToForgotPassword()");
            _changeViewModelManually(RequestedViewToGO.ForgotPasswordVM);
        }

        bool CanGoToForgotPassword()
        {
            return true;
        }

        #endregion // Private Helpers

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
