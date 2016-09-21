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

    public enum RequestedViewToGO
    {
        /*
            The changes of view we can perform manually are:
            - Login to forgot password.
            - Forgot password to Login.
            - Register to Login.
            Those can be summarized to:
            - Go to forgot password.
            - Go to Login.
         */
        LoginVM, ForgotPasswordVM
    };

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

            My thought of tackling this is:
            - Find a way to synchronize actions (commands of the views) so whenever one is clicked those are propagated via events,
            therefore this ViewModel can know what and when it happened, that way we can change the ViewModels here.
            So basically if we click the forgot password link in the Login, sessionviewModel will know this and change the ViewModel
            to ForgotPassword.
            I think all this is achievable using events.

            Second Thought:
            I think we were thinkging wrong about events: Events are great to keep synchronized a lot of classes and calling a
            collection of methods (delegates) when something significant happens. In our case would imply having an event here,
            then make that event (through an interface) public, then the Login and ohers views will subcribe with a method.
            But that will cause to execute all the methods when the event happens. We don't want that.

            So after give it a thought, I think it will be better to create a static method here that will change the view manually.
            We can specify to which view change by creating an enum. The the Login, Register, and ForgotPassword will call this method
            passing the enum parameter.

             */

        #region Fields

        ICommand _changePageCommand;
        IPageViewModel _currentPageViewModel;
        List<IPageViewModel> _pageViewModels;

        static IPageViewModel[] _mapPageViewModels;

        

        #endregion // Fields

        #region Constructors

        public SessionViewModel()
        {
            PageViewModels.Add(new LoginViewModel(this.GoToViewModel));
            PageViewModels.Add(new RegisterViewModel(this.GoToViewModel));
            PageViewModels.Add(new ForgotPasswordViewModel());

            CurrentPageViewModel = PageViewModels[0];

            InitMapPageViewModels();
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

        public void GoToViewModel(RequestedViewToGO viewModel)
        {
            switch (viewModel)
            {
                case RequestedViewToGO.LoginVM:
                    ChangeViewModel(_mapPageViewModels[0]);
                    break;
                case RequestedViewToGO.ForgotPasswordVM:
                    ChangeViewModel(_mapPageViewModels[2]);
                    break;
            }
        }

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
            {
                PageViewModels.Add(viewModel);
            }

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        private void InitMapPageViewModels()
        {
            _mapPageViewModels = new IPageViewModel[PageViewModels.Count];

            for (int i = 0; i < PageViewModels.Count; ++i)
            {
                _mapPageViewModels[i] = PageViewModels[i];
            }
        }

        #endregion // Methods

    }
}
