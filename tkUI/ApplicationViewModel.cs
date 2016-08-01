using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input;
using tkUI.Helper_Classes;
using tkUI.ViewModels;
using tkUI.DataAccess;

namespace tkUI
{
    public class ApplicationViewModel : ObservableObject
    {
        #region Fields

        readonly EmployeeRepository _employeeRepository;

        private ICommand _changePageCommand;
        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;

        #endregion

        #region Constructors

        public ApplicationViewModel(string customerDataFile)
        {
            // Load XML file
            _employeeRepository = new EmployeeRepository(customerDataFile);

            // Add available pages
            PageViewModels.Add(new DashboardViewModel() { Checked = true }); // Checked is true since it's the default button selected
            PageViewModels.Add(new EmployeesViewModel(_employeeRepository));
            PageViewModels.Add(new PaymentsViewModel());
            PageViewModels.Add(new DocumentsViewModel());
            PageViewModels.Add(new SearchViewModel());
            PageViewModels.Add(new ConfigurationViewModel());
            PageViewModels.Add(new AdministrationViewModel());
            /*PageViewModels.Add(new HomeViewModel());
            PageViewModels.Add(new ProductsViewModel());*/

            // Set starting page
            CurrentPageViewModel = PageViewModels[0];
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
                        p => ChangeViewModel((IPageViewModel)p),
                        p => p is IPageViewModel);
                }

                return _changePageCommand;
            }
        }

        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<IPageViewModel>();

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

        #endregion

        #region Methods

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        #endregion
    }
}
