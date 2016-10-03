using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input;
using tkUI.Helper_Classes;
using tkUI.Subpages.GraphQuickBoxes.Utils;
using tkUI.Subpages.EmployeesCRUD.ViewModels;
using tkUI.Models;
using tkUI.DataAccess;

namespace tkUI.ViewModels
{
    class EmployeesViewModel : PageFromNavigation
    {

        #region Fields

        private ICommand _changePageCommand;

        private IButton _currentPageViewModel;
        private List<IButton> _pageViewModels;


        //TODO: Track down this behaviour.
        /* Note: Since we need to make the first QuickBox IsChecked = true to show the border in the selected state,
         * we set the property Checked of the first ViewModel to true. But this causes to call the ChangePageCommand
         * before the View is rendered (and therefore the Chart added to the container) causing to lose this object.
         * So when the ChangePageCommand is called for first time (this is done automatically by the IsChecked property)
         * we don't actually execute the command by using the bool _avoidChange.
         */
        static bool _avoidChange;

        #endregion // Fields

        #region Constructors

        public EmployeesViewModel(EmployeeRepository employeeRepository)
        {
            // Add available Pages
            PageViewModels.Add(new ListEmployeesViewModel(employeeRepository) { Checked = true});
            //PageViewModels.Add(new AddEmployeeViewModel());
            PageViewModels.Add(new EmployeeWrapperViewModel(Employee.CreateNewEmployee(), employeeRepository));

            // TODO: Delete AdminViewModel in the future
            /*PageViewModels.Add(new ListAdminViewModel());
            PageViewModels.Add(new AddAdminViewModel());*/

            // Set default view
            CurrentPageViewModel = PageViewModels[0];
        }

        #endregion // Commands

        #region Interface Implementations
        public override string Name
        {
            get
            {
                return "Empleados";
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
                return "Resources/IconsNavigation/HumanIcon.png";
            }
        }

        public override string Selected
        {
            get
            {
                return "Resources/IconsNavigation/HumanSelectedIcon.png";
            }
        }

        #endregion // Interface Implementations

        #region Properties / Commands

        public ICommand ChangePageCommand
        {
            get
            {
                if (_changePageCommand == null || !_avoidChange) // _avoidChange -> See field definition for more info
                {
                    _changePageCommand = new RelayCommand(
                        p => ChangeViewModel((IButton)p),
                        p => p is IButton);
                    _avoidChange = true;
                }

                return _changePageCommand;
            }
        }

        public List<IButton> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<IButton>();

                return _pageViewModels;
            }
        }

        public IButton CurrentPageViewModel
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

        private void ChangeViewModel(IButton viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        #endregion

    }
}
