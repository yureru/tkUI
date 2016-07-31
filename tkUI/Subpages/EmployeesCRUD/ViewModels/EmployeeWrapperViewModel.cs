using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Windows.Input;

using tkUI.Models;
using tkUI.DataAccess;
using tkUI.Helper_Classes;

using tkUI.Subpages.EmployeesCRUD.Utils;

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
        bool _isSelected;
        RelayCommand _saveCommand;

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
            _genderType = "(Sin Especificar)";

        }

        #endregion // Constructors

        #region Employee Properties

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

        public bool Gender
        {
            get { return _employee.Gender; }
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
                ///TODO: Use a ResourceDictionary to save the strings
                if (_genderType == "Hombre")
                {
                    _employee.Gender = false;
                }
                else if (_genderType == "Mujer")
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
                        // To strings resource
                        "(Sin Especificar)", "Hombre", "Mujer"
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
                    // To strings resource
                    return "Añadir Empleado";
                }
                else
                {
                    return String.Format("{0}, {1}", _employee.LastName, _employee.FirstName);
                }
            }
        }

        // Not used:
        // public bool IsSelected

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

        #endregion // Presentations Properties

        #region Private Methods

        /// <summary>
        /// Saves the customer to the repository. This method
        /// </summary>
        public void Save()
        {
            if (!_employee.IsValid)
            {
                // To strings resource
                throw new InvalidOperationException("No se puede guardar un empleado inválido.");
            }
            if (this.IsNewEmployee)
            {
                _employeeRepository.AddEmployee(_employee);
            }

            base.OnPropertyChanged("DisplayName");
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
        ///TODO: Validate Gender combobox here
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
            // To strings resource
            if (this.GenderType == "Mujer" || this.GenderType == "Hombre")
            {
                return null;
            }
            // To strings resource
            return "El género del empleado debe seleccionarse";
        }

        #endregion // Interface Implementations

    }
}
