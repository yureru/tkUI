using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Input;

using tkUI.DataAccess;
using tkUI.Subpages.EmployeesCRUD.Utils;
using tkUI.Helper_Classes;
using tkUI.Properties;

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
        RelayCommand _deleteRangeUsers;
        RelayCommand _saveChangesCommand;

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

        public ICommand DeleteRangeUsers
        {
            get
            {
                if (_deleteRangeUsers == null)
                {
                    _deleteRangeUsers = new RelayCommand(
                        param => this.DeleteFromRange(),
                        param => this.CanDeleteFromRange()
                        );
                }
                return _deleteRangeUsers;
            }
        }

        public ICommand SaveChangesCommand
        {
            get
            {
                if (_saveChangesCommand == null)
                {
                    _saveChangesCommand = new RelayCommand(
                        param => this.SaveChanges(),
                        param => this.CanSaveChanges()
                        );
                }
                return _saveChangesCommand;
            }
        }

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

        /// <summary>
        /// Tooltip to determine how many employees we're going to delete.
        /// </summary>
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

        #region Methods

        /// <summary>
        /// Deletes a selected range of employees. If the range contains an employee with a edit modal open, shows a warning and
        /// tells it can't perform that action.
        /// It shows a confirmation dialog if the range is valid.
        /// </summary>
        public void DeleteFromRange()
        {
            // Check if there's an employee with an edit modal dialog open
            // isModalOpen will contain an employee if it has a the edit modal open.
            // Note that we're comparing to false, see EditModalOpen for full explanation.
            //var isEditModalOpen = from emp in this.AllEmployees where emp.EditModalOpen == false select emp;
            var isEditModalOpen = from emp in this.AllEmployees where emp.EditModalOpen == false where emp.IsSelected select emp;

            // Show error message
            if (isEditModalOpen.Count() > 0)
            {
                var warningResult = MessageBox.Show(Resources.ListEmployeesViewModel_Warning_InvalidRange, Resources.ListEmployeesViewModel_Warning_Self,
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Ok, we can delete a range.
            var result = MessageBox.Show(String.Format(Resources.ListEmployeesViewModel_Format_ConfirmationRange, TotalSelectedEmployees), 
                Resources.ListEmployeesViewModel_Confirmation_Self, MessageBoxButton.YesNo, MessageBoxImage.None);

            if (result == MessageBoxResult.No || result == MessageBoxResult.None)
            {
                //TODO: Unselect the previously selected items.
                //TODO: Buttons will show "Si" and "No" instead of "Yes" or "No", this seems like a lot of work for little profit.
                return;
            }

            var selectedEmps = (from emp in this.AllEmployees where emp.IsSelected select emp.ID).ToList();

            foreach (var emp in selectedEmps)
            {
                try
                {
                    _employeeRepository.DeleteByID(emp);
                }
                catch (ArgumentOutOfRangeException)
                {

                }
            }
        }

        /// <summary>
        /// Checks if the command is available.
        /// </summary>
        /// <returns></returns>
        public bool CanDeleteFromRange()
        {
            if (TotalSelectedEmployees > 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SaveChanges()
        {
            _employeeRepository.SaveEmployees();
        }

        bool CanSaveChanges()
        {
            // TODO: Can't save if an employee is being edited.
            return true;
        }

        #endregion // Methods
    }
}
