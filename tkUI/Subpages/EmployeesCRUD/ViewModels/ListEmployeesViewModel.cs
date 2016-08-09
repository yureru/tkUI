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
using tkUI.Helper_Classes;

using System.Diagnostics;

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
        RelayCommand _deleteCommand;

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

            // Subscribe for notifications when an Employee is deleted.
            _employeeRepository.EmployeeDeleted += this.OnEmployeeDeletedInRepository;

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

        #region Commands

        

        #endregion // Commands

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

        /// <summary>
        /// Search for the Employee with the given ID and deletes it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnEmployeeDeletedInRepository(object sender, EmployeeDeletedEventArgs e)
        {
            for (int i = 0; i < this.AllEmployees.Count; ++i)
            {
                if (this.AllEmployees[i].ID == e.ID)
                {
                    this.AllEmployees.RemoveAt(i);
                    break;
                }
            }

        }

        #endregion // Event Handling Methods
    }
}
