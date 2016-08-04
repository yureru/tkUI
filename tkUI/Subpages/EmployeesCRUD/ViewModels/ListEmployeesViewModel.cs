using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using System.Collections.ObjectModel;
using System.Collections.Specialized;

using tkUI.DataAccess;
using tkUI.Subpages.EmployeesCRUD.Utils;



namespace tkUI.Subpages.EmployeesCRUD.ViewModels
{
    /// <summary>
    /// Represents a container of EmployeeWrapperViewModel objects
    /// that has support for staying synchronized with the
    /// EmployeeRepository. This class also provides means to edit
    /// and delete employees.
    /// </summary>
    class ListEmployeesViewModel : ObservablePageFromCRUD
    {

        #region Fields

        readonly EmployeeRepository _employeeRepository;

        #endregion // Fields

        #region Constructors

        public ListEmployeesViewModel(EmployeeRepository employeeRepository)
        {
            if (employeeRepository == null)
            {
                throw new ArgumentNullException("employeeRepository");
            }

            _employeeRepository = employeeRepository;

            // Subscribe for notifications of when a new employee is saved.
            _employeeRepository.EmployeeAdded += this.OnEmployeeAddedToRepository;

            // Populate AllEmployees collection with EmployeeWrapperViewModels.
            this.CreateAllEmployees();
        }

        void CreateAllEmployees()
        {
            List<EmployeeWrapperViewModel> all =
                (from emp in _employeeRepository.GetEmployees()
                 select new EmployeeWrapperViewModel(emp, _employeeRepository)).ToList();

            foreach (EmployeeWrapperViewModel evm in all)
            {
                evm.PropertyChanged += this.OnEmployeeViewModelPropertyChanged;
            }

            this.AllEmployees = new ObservableCollection<EmployeeWrapperViewModel>(all);
            this.AllEmployees.CollectionChanged += this.OnCollectionChanged;
        }

        #endregion // Constructors

        #region Interface Implementations

        public override string Name
        {
            get
            {
                return "Ver empleados";
            }
        }

        /// <summary>
        /// Returns a collection of all the EmployeeWrapperViewModel objects.
        /// </summary>
        public ObservableCollection<EmployeeWrapperViewModel> AllEmployees { get; private set; }

        // TotalSelectedSales not used.

        #endregion // Interface Implementations

        #region Event Handling Methods

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
            {
                foreach (EmployeeWrapperViewModel empVM in e.NewItems)
                    empVM.PropertyChanged += this.OnEmployeeViewModelPropertyChanged;
            }

            if (e.OldItems != null && e.OldItems.Count != 0)
            {
                foreach (EmployeeWrapperViewModel empVM in e.OldItems)
                    empVM.PropertyChanged -= this.OnEmployeeViewModelPropertyChanged;
            }
        }

        void OnEmployeeViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ///TODO: This could serve as a select several users and delete them.
            
        }

        void OnEmployeeAddedToRepository(object sender, EmployeeAddedEventArgs e)
        {
            var viewModel = new EmployeeWrapperViewModel(e.NewEmployee, _employeeRepository);
            this.AllEmployees.Add(viewModel);
        }

        #endregion // Event Handling Methods
    }
}
