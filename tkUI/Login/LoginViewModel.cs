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
            return true;
        }

        #endregion // Private Helpers

    }
}
