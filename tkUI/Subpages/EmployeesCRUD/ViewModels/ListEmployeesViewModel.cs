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
        bool _enableRangeDelete;

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

        #endregion // Interface Implementations

        #region Public Interface

        /// <summary>
        /// Returns a collection of all the EmployeeWrapperViewModel objects.
        /// </summary>
        public ObservableCollection<EmployeeWrapperViewModel> AllEmployees { get; private set; }

        public int TotalSelectedEmployees
        {
            get
            {
                return this.AllEmployees.Sum(empVM => empVM.IsSelected ? 1 : 0);
            }
        }

        /*public string RangeDeleteTooltip
        {
            get
            {
                return _rangeDeleteTooltip;
            }

            set
            {
                if (_rangeDeleteTooltip == value)
                {
                    return;
                }

                _rangeDeleteTooltip = value;
                OnPropertyChanged("RangeDeleteTooltip");
            }
        }*/

        /*public string RangeDeleteTooltip
        {
            get
            {
                return UpdateTooltip();
            }
            set
            {
                _rangeDeleteTooltip = value;
            }
        }*/

        public string RangeDeleteTooltip
        {
            get
            {
                if (TotalSelectedEmployees < 2)
                {
                    return "";
                }
                return "Eliminar " + TotalSelectedEmployees + " empleados";
            }
        }

        #endregion // Public Interface

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
            string IsSelected = "IsSelected";
            // Make sure that the property name we're referencing is valid.
            // This is a debugging technique, and does not execute in a Release build.
            (sender as EmployeeWrapperViewModel).VerifyPropertyName(IsSelected);

            // When a customer is selected or unselected, we must let the
            // world know that the TotalSelectedSales property has changed,
            // so that it will be queried again for a new value.
            if (e.PropertyName == IsSelected)
            {
                this.OnPropertyChanged("TotalSelectedEmployees");
                this.OnPropertyChanged("RangeDeleteTooltip");
            }
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
                    /* Update this property here because we could select some employees, then delete one,
                     * and therefore the selected quantity will still be the same value. This prevents that. */
                    this.OnPropertyChanged("TotalSelectedEmployees");
                    
                    break;
                }
            }

        }

        #endregion // Event Handling Methods
    }
}
