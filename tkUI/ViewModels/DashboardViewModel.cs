using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using tkUI.Helper_Classes;
using tkUI.Subpages.GraphQuickBoxes.Utils;
using tkUI.Subpages.GraphQuickBoxes.ViewModels;

namespace tkUI.ViewModels
{
    /*lass DashboardViewModel : ObservableObject, IPageViewModel, ISourceIcons*/
    class DashboardViewModel : PageFromNavigation
    {

        #region Fields

        private ICommand _changePageCommand;

        private IBoxes _currentPageViewModel;
        private List<IBoxes> _pageViewModels;

        static int _UID;

        #endregion // Fields

        #region Commands

        public DashboardViewModel()
        {
            // Add available Pages
            PageViewModels.Add(new ExpectedPaymentViewModel());
            PageViewModels.Add(new EmployeesInCompanyViewModel());
            PageViewModels.Add(new EmployeesHiredViewModel());

            // Set default graph
            CurrentPageViewModel = PageViewModels[0];
        }

        #endregion // Commands

        #region Interface Implementations
        public override string Name
        {
            get
            {
                return "Dashboard";
            }
        }

        public override string Clicked
        {
            get
            {
                return "";
            }
        }

        public override string Hover
        {
            get
            {
                return "";
            }
        }


        public override string Normal
        {
            get
            {
                return "Resources/IconsNavigation/DashboardIcon.png";
            }
        }

        public override string Selected
        {
            get
            {
                return "Resources/IconsNavigation/DashboardSelectedIcon.png";
            }
        }

        #endregion // Interface Implementations


        #region Properties / Commands

        public ICommand ChangePageCommand
        {
            get
            {
                if (_changePageCommand == null)
                {
                    _changePageCommand = new RelayCommand(
                        p => ChangeViewModel((IBoxes)p),
                        p => p is IBoxes);
                }

                return _changePageCommand;
            }
        }

        public List<IBoxes> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<IBoxes>();

                return _pageViewModels;
            }
        }

        public IBoxes CurrentPageViewModel
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

        public int IDColumn
        {
            get { return _UID++; }
        }

        #endregion

        #region Methods

        private void ChangeViewModel(IBoxes viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        #endregion

    }
}
