using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input;
using tkUI.Helper_Classes;

namespace tkUI.Login
{
    class LoginViewModel : ObservableObject
    {

        /* TODO: Following tasks for Login
         * 1- Set names for the textbox fields, and the function validations.
         * 2- The content presenters to show the error messages, this messages should appear only after clicking the "Ingresar" button.
         * 3- Start implementing the login code (email and password validators).
         
             */

        #region Fields

        RelayCommand _loginCommand;

        #endregion // Fields


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

        #endregion // Commands

        #region Private Helpers

        void Login()
        {
            tkUI.App.RunAppAfterSuccessfulLogin();
        }

        bool CanLogin()
        {
            // TODO: Mail and Password fields are filled?
            if (LoginView.Pass != null && LoginView.Pass.Length != 0)
            {
                return true;
            }

            return false;
        }

        #endregion // Private Helpers

    }
}
