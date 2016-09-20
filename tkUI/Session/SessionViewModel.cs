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

            CurrentPageViewModel = PageViewModels[0];

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
