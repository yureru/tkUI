using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input;
using tkUI.Helper_Classes;
using tkUI.Session.ViewModels;


using tkUI.Session.Utils;

namespace tkUI.Session
{

    /// <summary>
    /// Specifies to which view we desire to go.
    /// </summary>
    public enum RequestedViewToGO
    {
        LoginVM, ForgotPasswordVM
    };

    class SessionViewModel : ObservableObject
    {

        // TODO: Abstract (DRY) the functionality needed to implement navigation in a MVVM way.

        #region Fields

        ICommand _changePageCommand;
        IPageViewModelWithSizes _currentPageViewModel;
        List<IPageViewModelWithSizes> _pageViewModels;

        static IPageViewModelWithSizes[] _mapPageViewModels;

        #endregion // Fields

        #region Constructors

        public SessionViewModel()
        {
            var LoginVM = new LoginViewModel(this.GoToViewModel);

            PageViewModels.Add(LoginVM);
            //PageViewModels.Add(new RegisterViewModel(this.GoToViewModel));
            PageViewModels.Add(new RegisterFirstAdminViewModel(this.GoToViewModel, LoginVM.SetSuccessMessage));
            PageViewModels.Add(new ForgotPasswordViewModel(this.GoToViewModel, LoginVM.SetSuccessMessage));

            // TODO: Has an admin been ever registered?, If not choose the RegisterViewModel as default.
            CurrentPageViewModel = PageViewModels[0];

            InitMapPageViewModels();
        }

        #endregion // Constructors

        #region Properties / Commands

        public ICommand ChangePageCommand
        {
            get
            {
                if (_changePageCommand == null)
                {
                    _changePageCommand = new RelayCommand(
                        p => ChangeViewModel((IPageViewModelWithSizes)p),
                        p => p is IPageViewModelWithSizes
                        );
                }

                return _changePageCommand;
            }
        }

        public List<IPageViewModelWithSizes> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                {
                    _pageViewModels = new List<IPageViewModelWithSizes>();
                }
                return _pageViewModels;
            }
        }

        public IPageViewModelWithSizes CurrentPageViewModel
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

        /// <summary>
        /// Function to be passed as Action to the ViewModels that needs to change
        /// the View manually.
        /// Note that this assumes LoginVM case it's the LoginViewModel at index [0], 
        /// and ForgotPasswordVM is at index [2].
        /// </summary>
        /// <param name="viewModel">ViewModel to go.</param>
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

        
        private void ChangeViewModel(IPageViewModelWithSizes viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
            {
                PageViewModels.Add(viewModel);
            }

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        /// <summary>
        /// This maps the current ViewModels added to the PageViewModels so we can refer them
        /// later when using GoToViewModel function.
        /// </summary>
        private void InitMapPageViewModels()
        {
            _mapPageViewModels = new IPageViewModelWithSizes[PageViewModels.Count];

            for (int i = 0; i < PageViewModels.Count; ++i)
            {
                _mapPageViewModels[i] = PageViewModels[i];
            }
        }

        #endregion // Methods

    }
}
