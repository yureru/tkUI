using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Windows.Input;
using System.Windows;

using tkUI.Models;
using tkUI.DataAccess;
using tkUI.Helper_Classes;

using System.Diagnostics;


using tkUI.Subpages.EmployeesCRUD.Utils;
using tkUI.Properties;
using tkUI.Subpages.EmployeesCRUD.Views;

namespace tkUI.Subpages.EmployeesCRUD.ViewModels
{
    /// <summary>
    /// A UI-friendly wrapper for a Employee object. This can be consumed
    /// by AddEmployee and ListEmployees ViewModels.
    /// </summary>
    class EmployeeWrapperViewModel : ObservablePageFromCRUD, IDataErrorInfo
    {

        #region Fields

        readonly Employee _employee;
        readonly EmployeeRepository _employeeRepository;
        string _genderType;
        string[] _genderTypeOptions;
        string _lastUserSaved;
        bool _isSelected;
        RelayCommand _saveCommand;
        RelayCommand _deleteCommand;
        RelayCommand _editCommand;

        #endregion // Fields

        #region Constructors

        public EmployeeWrapperViewModel(Employee employee, EmployeeRepository employeeRepository)
        {
            if (employee == null)
            {
                throw new ArgumentNullException("employee");
            }

            if (employeeRepository == null)
            {
                throw new ArgumentNullException("employeeRepository");
            }

            _employee = employee;
            _employeeRepository = employeeRepository;
            _genderType = Resources.EmployeeWrapperViewModel_GenderTypeOptions_NotSpecified;

        }

        #endregion // Constructors

        #region Employee Properties

        public int ID
        {
            get { return _employee.ID; }
            set { }
        }

        public string FirstName
        {
            get { return _employee.FirstName; }
            set
            {
                if (value == _employee.FirstName)
                {
                    return;
                }

                _employee.FirstName = value;

                base.OnPropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get { return _employee.LastName; }
            set
            {
                if (_employee.LastName == value)
                {
                    return;
                }

                _employee.LastName = value;

                base.OnPropertyChanged("LastName");
            }
        }

        public string Gender
        {
            get
            {
                if (_employee.Gender)
                {
                    return Resources.EmployeeWrapperViewModel_GenderTypeOptions_Female;
                }
                return Resources.EmployeeWrapperViewModel_GenderTypeOptions_Male;
            }
        }

        #endregion // Employee Properties

        #region Presentation Properties

        public string GenderType
        {
            get { return _genderType; }
            set
            {
                if (_genderType == value || String.IsNullOrEmpty(value))
                {
                    return;
                }

                _genderType = value;

                if (_genderType == Resources.EmployeeWrapperViewModel_GenderTypeOptions_Male)
                {
                    _employee.Gender = false;
                }
                else if (_genderType == Resources.EmployeeWrapperViewModel_GenderTypeOptions_Female)
                {
                    _employee.Gender = true;
                }

                base.OnPropertyChanged("GenderType");
            }
        }

        public string[] GenderTypeOptions
        {
            get
            {
                if (_genderTypeOptions == null)
                {
                    _genderTypeOptions = new string[]
                    {
                        Resources.EmployeeWrapperViewModel_GenderTypeOptions_NotSpecified,
                        Resources.EmployeeWrapperViewModel_GenderTypeOptions_Male,
                        Resources.EmployeeWrapperViewModel_GenderTypeOptions_Female
                    };
                }
                return _genderTypeOptions;
            }
        }

        public string DisplayName
        {
            get
            {
                if (this.IsNewEmployee)
                {
                    return Resources.EmployeeWrapperViewModel_AddEmployee;
                }
                else
                {
                    return String.Format("{0}, {1}", _employee.LastName, _employee.FirstName);
                }
            }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value == _isSelected)
                {
                    return;
                }

                _isSelected = value;

                base.OnPropertyChanged("IsSelected");
            }
        }

        /// <summary>
        /// Used to show which user is currently/last saved.
        /// </summary>
        public string LastUserSaved
        {
            get { return _lastUserSaved; }
            set
            {
                if (_lastUserSaved != value)
                {
                    _lastUserSaved = value;
                    base.OnPropertyChanged("LastUserSaved");
                }
            }
        }

        /// <summary>
        /// Returns a command that saves the customer.
        /// </summary>
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(
                        param => this.Save(),
                        param => this.CanSave
                        );
                }
                return _saveCommand;
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                {
                    _deleteCommand = new RelayCommand(
                        param => this.Delete(param),
                        param => this.CanDelete()
                        );
                }
                return _deleteCommand;
            }

        }

        public ICommand EditCommand
        {
            get
            {
                if (_editCommand == null)
                {
                    _editCommand = new RelayCommand(
                        param => this.ShowEditDialog(param),
                        param => this.CanEdit()
                        );
                }
                return _editCommand;
            }
        }

        #endregion // Presentations Properties

        #region Private Methods

        /// <summary>
        /// Saves the customer to the repository. Creates a new Employee to add it.
        /// </summary>
        public void Save()
        {
            bool flag = false;
            if (!_employee.IsValid)
            {
                throw new InvalidOperationException(Resources.EmployeeWrapperViewModel_Exception_CannotSave);
            }

            if (this.IsNewEmployee)
            {
                var newEmployee = Employee.CreateEmployee(_employee);
                _employeeRepository.AddEmployee(newEmployee);
                SetLastUserSaved(false);
                CleanForm();
                flag = true;
            }

            // The user was saved in the ListEmployeeView/Edit button.
            if (!flag)
            {
                SetLastUserSaved(true);
                base.OnPropertyChanged("Gender");
            }
            base.OnPropertyChanged("DisplayName");
        }

        public void Delete(object id)
        {
            if (!(id is int))
            {
                throw new ArgumentException("Param passed to DeleteCommand should be integer.");
            }

            _employeeRepository.DeleteByID((int) id);
        }

        #endregion // Private Methods

        #region Private Helpers

        /// <summary>
        /// Returns true if this customer was created by the user and it has not yet
        /// been saved to the customer repository.
        /// </summary>
        bool IsNewEmployee
        {
            get { return !_employeeRepository.ContainsEmployee(_employee); }
        }

        bool CanSave
        {
            get { return String.IsNullOrEmpty(this.ValidateGenderType()) && _employee.IsValid; }
        }

        bool CanDelete()
        {
            return true;
        }

        /// <summary>
        /// Cleans the UI forms. That way the user can enter new data without cleaning
        /// himself the controls.
        /// </summary>
        void CleanForm()
        {
            FirstName = null;
            LastName = null;
            _genderType = Resources.EmployeeWrapperViewModel_GenderTypeOptions_NotSpecified;
            base.OnPropertyChanged("GenderType");
        }

        /// <summary>
        /// Sets the message when an user is being saved.
        /// </summary>
        void SetLastUserSaved(bool IsEditingEmployee)
        {
            if (IsEditingEmployee) {
                if (Gender == Resources.EmployeeWrapperViewModel_GenderTypeOptions_Female)
                {
                    LastUserSaved = String.Format("La empleada {0}, {1} fue editada exitosamente", LastName, FirstName);
                }
                else
                {
                    LastUserSaved = String.Format("El empleado {0}, {1} fue editado exitosamente", LastName, FirstName);
                }
            }
            else
            {
                if (Gender == Resources.EmployeeWrapperViewModel_GenderTypeOptions_Female)
                {
                    LastUserSaved = String.Format("La empleada {0}, {1} fue guardada exitosamente", LastName, FirstName);
                }
                else
                {
                    LastUserSaved = String.Format("El empleado {0}, {1} fue guardado exitosamente", LastName, FirstName);
                }
            }
        }

        /// <summary>
        /// Method that shows a modal dialog that allow us to edit an employee.
        /// Currently it's using the Show() so it doesn't blocks.
        /// </summary>
        /// <param name="id"></param>
        void ShowEditDialog(object id)
        {
            if (!(id is int))
            {
                throw new ArgumentException("Param passed to EditCommand should be integer.");
            }

            Window modal = new Window();
            // Create the forms to edit
            var view = new AddEmployeeView();
            modal.Width = 450;
            modal.Height = 350;

            // Search for the employee by id
            var listEmp = _employeeRepository.GetEmployees();
            var employeeEdited = (from emps in listEmp where emps.ID.Equals(id) select emps).ToList();

            // Gender == true means Female
            if (employeeEdited.Count > 0 && employeeEdited[0].Gender)
            {
                this.GenderType = Resources.EmployeeWrapperViewModel_GenderTypeOptions_Female;
            }
            else
            {
                this.GenderType = Resources.EmployeeWrapperViewModel_GenderTypeOptions_Male;
            }
            
            modal.DataContext = this;
            
            modal.Content = view;
            modal.Title = "Edit Employee";
            //modal.ShowDialog();
            modal.Show();

            // Check if the fields are different to the original Employee element, if so:
            // 1. The Save button can performs.
            // 2. If the users close the window without saving ask him if he wanna save
        }

        bool CanEdit()
        {
            return true;
        }

        #endregion // Private Helpers


        #region Interface Implementations

        public override string Name
        {
            get
            {
                return "+ Añadir empleado";
            }
        }

        string IDataErrorInfo.Error
        {
            get { return (_employee as IDataErrorInfo).Error; }
        }

        // Used to validate an unselected Combobox.
        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                string error = null;

                if (propertyName == "GenderType")
                {
                    error = this.ValidateGenderType();
                }
                else
                {
                    error = (_employee as IDataErrorInfo)[propertyName];
                }

                CommandManager.InvalidateRequerySuggested();

                return error;
            }
        }

        string ValidateGenderType()
        {
            if (this.GenderType == Resources.EmployeeWrapperViewModel_GenderTypeOptions_Female
                || this.GenderType == Resources.EmployeeWrapperViewModel_GenderTypeOptions_Male)
            {
                return null;
            }
            
            return Resources.EmployeeWrapperViewModel_Error_MissingGenderType;
        }

        #endregion // Interface Implementations

    }
}
