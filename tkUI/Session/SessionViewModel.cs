using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input;
using tkUI.Helper_Classes;
using tkUI.Session.ViewModels;


namespace tkUI.Session
{
    class SessionViewModel : ObservableObject
    {

        // TODO: Abstract (DRY) the functionality needed to implement navigation in a MVVM way.

        /*
            Now that I think about it, there's some scenarios while login in the app.
            1- The first admin user hasn't been registered. Therefore needs the following things to be made:
                a) The first View showed is the Register one, after enter all the credentials and those being validated, this view is not
                going to be used anymore. And the view will change to the "Login" one.

            2- The admin or another user has been registered already, therefore logins normally. After login the App spawns.

            3- The admon or another user has been registered already, but they have forgotten the password. So they click the "forgot password"
            link, then that View is showed and they enter the email. After that, it shows a message of sent the new password set or w/e.

            Therefore:
            Register needs to show the Login view after creating the first user.
            Login needs a link to allow changing the view to Forgot password.
            And forgot password needs a button to go backwards to Login view, and to show the Login after sending the email.

             */

        #region Fields

        ICommand _changePageCommand;
        IPageViewModel _currentPageViewModel;
        List<IPageViewModel> _pageViewModels;

        #endregion // Fields

        #region Constructors

        public SessionViewModel()
        {
            PageViewModels.Add(new LoginViewModel());
            PageViewModels.Add(new RegisterViewModel());
            PageViewModels.Add(new ForgotPasswordViewModel());

            CurrentPageViewModel = PageViewModels[2];

        }

        /*
         but what if you need several validations, and those validations are in a static class, they can't be passes as parameters
             */

        #endregion // Constructors

        #region Properties / Commands

        public ICommand ChangePageCommand
        {
            get
            {
                if (_changePageCommand == null)
                {
                    _changePageCommand = new RelayCommand(
                        p => ChangeViewModel((IPageViewModel)p),
                        p => p is IPageViewModel
                        );
                }

                return _changePageCommand;
            }
        }

        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                {
                    _pageViewModels = new List<IPageViewModel>();
                }
                return _pageViewModels;
            }
        }

        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }

            set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    OnPropertyChanged("CurrentPageViewModel");
                }
            }
        }

        #endregion // Properties Commands

        #region Methods

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
            {
                PageViewModels.Add(viewModel);
            }

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        #endregion // Methods

    }
}
