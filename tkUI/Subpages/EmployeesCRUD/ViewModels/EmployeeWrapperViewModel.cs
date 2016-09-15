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
        // TODO: Add a string resources that should be used only for the XAML and not for code-behind files.
        /* TODO:
         * 1- Re-design the SingleEmployeeView
             */

        #region Fields

        readonly Employee _employee;
        readonly EmployeeRepository _employeeRepository;
        static EmployeeWrapperViewModel _orignalData;
        string _genderType;
        string _selectedWorkTime;
        string _selectedDay, _selectedMonth, _selectedYear;
        string[] _genderTypeOptions;
        string[] _workTimeOptions;
        string _lastUserSaved;
        bool _isSelected;

        RelayCommand _saveCommand;
        RelayCommand _deleteCommand;
        RelayCommand _editCommand;
        RelayCommand _viewCommand;
        


        static EmployeeWrapperViewModel _editingCurrentEmployee;
        static bool _editingCurrentEmployeeIsInitialized;
        static bool _isEditingUser;
        static bool _isModalSpawned;
        static Employee _employeeBeingEdited;

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
            _genderType = Resources.EmployeeWrapperViewModel_ComboboxValue_NotSpecified;
            _selectedWorkTime = Resources.EmployeeWrapperViewModel_ComboboxValue_NotSpecified;
            this.Day = Resources.BirthDate_Combobox_Day;
            this.Month = Resources.BirthDate_Combobox_Month;
            this.Year = Resources.BirthDate_Combobox_Year;
        }

        #endregion // Constructors

        #region Employee Properties

        public int ID
        {
            get { return _employee.ID; }
            set { }
        }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
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

        public Birth Birthdate
        {
            get { return _employee.Birthdate; }
            set
            {
                if (_employee.Birthdate == value)
                {
                    return;
                }

                _employee.Birthdate = value;

                base.OnPropertyChanged("Birthdate");
            }
        }

        public string Day
        {
            get { return _selectedDay; }
            set
            {
                if (_selectedDay == value)
                {
                    return;
                }

                _selectedDay = value;

                OnPropertyChanged("Day");
            }
        }

        public string Month
        {
            get { return _selectedMonth; }
            set
            {
                if (_selectedMonth == value)
                {
                    return;
                }

                _selectedMonth = value;

                OnPropertyChanged("Month");
                OnPropertyChanged("Day"); // Day is updated here to allow validation whenever property Month changes.
            }
        }

        public string Year
        {
            get { return _selectedYear; }
            set
            {
                if (_selectedYear == value)
                {
                    return;
                }

                _selectedYear = value;

                OnPropertyChanged("Year");
                OnPropertyChanged("Day"); // Day is updated here to allow validation whenever property Year changes.
            }
        }

        public string Email
        {
            get { return _employee.Email; }
            set
            {
                if (_employee.Email == value)
                {
                    return;
                }

                _employee.Email = value;

                base.OnPropertyChanged("Email");
            }
        }

        public string Phone
        {
            get { return _employee.Phone; }
            set
            {
                if (_employee.Phone == value)
                {
                    return;
                }

                _employee.Phone = value;

                OnPropertyChanged("Phone");
            }
        }

        public string Pay
        {
            get { return _employee.Pay; }
            set
            {
                if (_employee.Pay == value)
                {
                    return;
                }

                _employee.Pay = value;

                OnPropertyChanged("Pay");
            }
        }

        public string PrettyPay
        {
            get
            {
                return String.Format("${0}.00", Pay);
            }
        }

        public string WorkTime
        {
            get { return _employee.WorkTime; }
        }

        public string Address
        {
            get { return _employee.Address; }
            set
            {
                if (_employee.Address == value)
                {
                    return;
                }

                _employee.Address = value;

                OnPropertyChanged("Address");
            }
        }

        public string Startedworking
        {
            get { return _employee.StartedWorking; }
            set { }
        }

        /// <summary>
        /// Checks if there's an edit modal open for a given employee instance.
        /// Note: EditModalOpen actually returns false (we're negatin ModalOpen) when
        /// the edit modal is open. That's cause we're binding this property to the
        /// ListEmployeeView and that needs the property IsEnabled="False" to know
        /// when the modal is open.
        /// </summary>
        public bool EditModalOpen
        {
            get
            {
                return !_employee.ModalOpen;
            }

            set
            {
                if (_employee.ModalOpen == value)
                {
                    return;
                }

                _employee.ModalOpen = value;
                OnPropertyChanged("EditModalOpen");
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
                        Resources.EmployeeWrapperViewModel_ComboboxValue_NotSpecified,
                        Resources.EmployeeWrapperViewModel_GenderTypeOptions_Male,
                        Resources.EmployeeWrapperViewModel_GenderTypeOptions_Female
                    };
                }
                return _genderTypeOptions;
            }
        }

        public string WorkTimeType
        {
            get { return _selectedWorkTime; }
            set
            {
                if (_selectedWorkTime == value || String.IsNullOrEmpty(value))
                {
                    return;
                }

                _selectedWorkTime = value;

                if (_selectedWorkTime != Resources.EmployeeWrapperViewModel_ComboboxValue_NotSpecified)
                {
                    _employee.WorkTime = _selectedWorkTime;
                }

                this.OnPropertyChanged("WorkTimeType");
            }
        }

        public string[] WorkTimeOptions
        {
            get
            {
                if (_workTimeOptions == null)
                {
                    _workTimeOptions = new string[]
                    {
                        Resources.EmployeeWrapperViewModel_ComboboxValue_NotSpecified,
                        Resources.EmployeeWrapperViewModel_WorkingTimeOptions_FullTime,
                        Resources.EmployeeWrapperViewModel_WorkingTimeOptions_PartTime
                    };
                }
                return _workTimeOptions;
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

        /* Days, Months, and Years are wrappers for the BirthDate Comboboxes */
        public static List<string> Days
        {
            get { return BirthDate.Days; }
        }

        public static string[] Months
        {
            get { return BirthDate.Months; }
        }

        public static List<string> Years
        {
            get { return BirthDate.Years; }
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

        public ICommand ViewCommand
        {
            get
            {
                if (_viewCommand == null)
                {
                    _viewCommand = new RelayCommand(
                        param => this.ShowViewDialog(param),
                        param => this.CanView()
                        );
                }
                return _viewCommand;
            }
        }

        

        

        #endregion // Presentations Properties

        #region ToolTips

        /* *** Currently Unused *** */
        public string EditToolTip
        {
            get
            {
                if (CanEdit())
                {
                    return Resources.EmployeeWrapperViewModel_ToolTip_EditButton_Enabled;
                }
                else
                {
                    return Resources.EmployeeWrapperViewModel_ToolTip_EditButton_Disabled;
                }
            }
        }

        public string DeleteToolTip
        {
            get
            {
                if (!_isModalSpawned)
                {
                    return Resources.EmployeeWrapperViewModel_ToolTip_DeleteButton_Enabled;
                }
                else
                {
                    return Resources.EmployeeWrapperViewModel_ToolTip_DeleteButton_Disabled;
                }
            }
        }

        #endregion // ToolTips

        #region Private Methods

        /// <summary>
        /// Saves the customer to the repository. Creates a new Employee to add it.
        /// It's also used when we edit an Employee.
        /// </summary>
        public void Save()
        {
            if (!_employee.IsValid)
            {
                throw new InvalidOperationException(Resources.EmployeeWrapperViewModel_Exception_CannotSave);
            }

            if (!_isEditingUser)
            {
                _employee.Birthdate = new Birth(); // TODO: Change this, I don't like allocating here, it should be handled in the class or the EmployeeRepository.
                _employee.Birthdate.SetDateWithValidatedInput(this.Day, this.Month, this.Year);
                var newEmployee = Employee.CreateEmployee(_employee);
                _employeeRepository.AddEmployee(newEmployee);
                SetLastUserSaved(false);
                CleanForm();
            }

            // The user was saved in the ListEmployeeView/Edit button.
            if (_isEditingUser)
            {
                Debug.Print("Saving edited user");
                SaveEmployeeBeingEdited(_editingCurrentEmployee, _employeeBeingEdited, _orignalData);
                SetLastUserSaved(true);
            }

        }

        public void Delete(object id)
        {
            if (!(id is int))
            {
                throw new ArgumentException(Resources.EmployeeWrapperViewModel_Exception_DeleteWrongParam);
            }

            _employeeRepository.DeleteByID((int) id);
        }

        /// <summary>
        /// Checks all the properties (primarly Comboboxes) to see if they're valid
        /// and therefore the employee can be saved.
        /// </summary>
        /// <returns>True if the fields are valid, false otherwise.</returns>
        bool FieldsAreValid()
        {
            return String.IsNullOrEmpty(this.ValidateGenderType())
                && _employee.IsValid
                && String.IsNullOrEmpty(this.ValidateWorkTime())
                && String.IsNullOrEmpty(BirthDate.ValidateBirthdate(this.Day, this.Month, this.Year));
        }

        /// <summary>
        /// Method that shows a modal dialog that allow us to edit an employee.
        /// Currently it's using the Show() so it doesn't blocks.
        /// </summary>
        /// <param name="id">Employee's ID</param>
        void ShowEditDialog(object id)
        {
            if (!(id is int))
            {
                throw new ArgumentException(Resources.EmployeeWrapperViewModel_Exception_EditWrongParam);
            }
            _isEditingUser = true;
            Window modal = new Window();
            // Create the forms to edit
            var view = new AddEmployeeView();
            modal.Width = 500;
            modal.Height = 400;
            modal.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            _isModalSpawned = true; // States a modal is currently being used.
            //OnPropertyChanged("EditToolTip");
            OnPropertyChanged("DeleteToolTip");
            modal.Activated += Modal_Activated;
            modal.Deactivated += Modal_Deactivated;
            modal.Closed += Modal_Closed;

            // Search for the employee by id
            var listEmp = _employeeRepository.GetEmployees();
            var employeeEdited = (from emps in listEmp where emps.ID.Equals(id) select emps).ToList();
            _employeeBeingEdited = employeeEdited[0];
            _orignalData = this;

            PrintEmployeeFields(employeeEdited[0]);

            // Initialize the Employee object that's going to be used as a temporal.
            if (!_editingCurrentEmployeeIsInitialized)
            {
                _editingCurrentEmployee = new EmployeeWrapperViewModel(Employee.CreateNewEmployee(), _employeeRepository);
                _editingCurrentEmployeeIsInitialized = true;
            }

            CopyEmployeeFields(employeeEdited[0], _editingCurrentEmployee);
            PrintEmployeeFields(_editingCurrentEmployee._employee);
            modal.DataContext = _editingCurrentEmployee;

            modal.Content = view;
            modal.Title = "Edit Employee";
            //modal.ShowDialog();
            modal.Show();

            // Check if the fields are different to the original Employee element, if so:
            // 1. The Save button can performs.
            // 2. If the users close the window without saving ask him if he wanna save
        }

        private void ShowViewDialog(object id)
        {
            if (!(id is int))
            {
                throw new ArgumentException(Resources.EmployeeWrapperViewModel_Exception_ViewWrongParam);
            }

            var modal = new Window();
            var view = new SingleEmployeeView();

            var employeeToShow = GetEmployeeByID((int)id);

            if (employeeToShow == null)
            {
                throw new ArgumentNullException(String.Format(Resources.EmployeeWrapperViewModel_Exception_EmployeeIDNotFound, (int)id));
            }

            var currentEmployee = new EmployeeWrapperViewModel(Employee.CreateNewEmployee(), _employeeRepository);
            CopyEmployeeFields(employeeToShow, currentEmployee);

            // Modal's chrome properties
            modal.Title = (currentEmployee.Gender == "Mujer" ? "Empleada: " : "Empleado: ") + currentEmployee.FullName;
            modal.Height = 500;
            modal.Width = 400;

            modal.DataContext = currentEmployee;
            modal.Content = view;

            modal.Show();
        }

        

        /// <summary>
        /// Searchs and returns an Employee by id.
        /// </summary>
        /// <param name="id">Integer of the ID, should be > 0.</param>
        /// <returns></returns>
        Employee GetEmployeeByID(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(Resources.EmployeeWrapperViewModel_Exception_IDLessOrEqualThanZero);
            }

            var listEmp = _employeeRepository.GetEmployees();
            var employeeEdited = (from emps in listEmp where emps.ID.Equals(id) select emps).ToList();

            if (employeeEdited.Count <= 0)
            {
                return null;
            }

            return employeeEdited[0];
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
            get { return FieldsAreValid(); }
        }

        bool CanDelete()
        {
            return true;
        }

        bool CanEdit()
        {
            if (!_isModalSpawned)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool CanView()
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
            Email = null;
            Phone = null;
            Pay = null;
            Address = null;
            this.GenderType = Resources.EmployeeWrapperViewModel_ComboboxValue_NotSpecified;
            this.WorkTimeType = Resources.EmployeeWrapperViewModel_ComboboxValue_NotSpecified;
            this.Day = Resources.BirthDate_Combobox_Day;
            this.Month = Resources.BirthDate_Combobox_Month;
            this.Year = Resources.BirthDate_Combobox_Year;
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

        /* *** Currently Unused *** */
        /// <summary>
        /// Saves a Birthdate when we're editing an user.
        /// </summary>
        /// <param name="item"></param>
        void SaveBirthdateToEmployee(Employee item)
        {
            if (this.Day == item.Birthdate.Day
                && this.Month == item.Birthdate.Month
                && this.Year == item.Birthdate.Year)
            {
                return;
            }

            item.Birthdate.SetDateWithValidatedInput(this.Day, this.Month, this.Year);
        }

        /* *** Currently unused *** */
        /// <summary>
        /// Loads the corresponding data for the Comboboxes in the modal edit dialog.
        /// That's because the textboxes are loaded automatically but the Comboboxes aren't (?)
        /// </summary>
        /// <param name="employeeEdited">The "list" of the employees, but it's actually a list with only one item.</param>
        void PopulateEditComboboxes(List<Employee> employeeEdited)
        {
            if (employeeEdited.Count <= 0)
            {
                Debug.Fail("Employee being edited doesn't exists");
                return;
            }

            LastUserSaved = null; // Cleans saved message after opening the Edit dialog.

            var current = employeeEdited[0];

            // Gender == true means Female
            if (current.Gender)
            {
                this.GenderType = Resources.EmployeeWrapperViewModel_GenderTypeOptions_Female;
            }
            else
            {
                this.GenderType = Resources.EmployeeWrapperViewModel_GenderTypeOptions_Male;
            }

            this.Day = current.Birthdate.Day;
            this.Month = current.Birthdate.Month;
            this.Year = current.Birthdate.Year;
            this.WorkTimeType = current.WorkTime;
            Debug.Print(current.WorkTime);
        }

        /// <summary>
        /// Copies the employee currently being edited to a temporal variable so it can be populated
        /// all the fields and Comboboxes of the edit modal dialog.
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="temp"></param>
        void CopyEmployeeFields(Employee employee, EmployeeWrapperViewModel temp)
        {
            temp.LastUserSaved = null;
            temp.FirstName = employee.FirstName;
            temp.LastName = employee.LastName;
            temp._employee.Gender = employee.Gender;
            temp.Day = employee.Birthdate.Day;
            temp.Birthdate = employee.Birthdate;
            temp.Month = employee.Birthdate.Month;
            temp.Year = employee.Birthdate.Year;
            temp.Email = employee.Email;
            temp.Phone = employee.Phone;
            temp.Pay = employee.Pay;
            temp._employee.WorkTime = employee.WorkTime;
            temp.Address = employee.Address;
            temp.WorkTimeType = employee.WorkTime;

            // Gender == true means Female
            if (employee.Gender)
            {
                temp.GenderType = Resources.EmployeeWrapperViewModel_GenderTypeOptions_Female;
            }
            else
            {
                temp.GenderType = Resources.EmployeeWrapperViewModel_GenderTypeOptions_Male;
            }
        }

        /// <summary>
        /// Saves the employee after clicking the Save button in the modal.
        /// </summary>
        /// <param name="newData">The wrapper of a temporal Employee object.</param>
        /// <param name="employee">The employee in the repository that is going to be actually saved</param>
        /// <param name="original">The original context where the employee resides, so it can nortify changes.</param>
        void SaveEmployeeBeingEdited(EmployeeWrapperViewModel newData, Employee employee, EmployeeWrapperViewModel original)
        {
            employee.FirstName = newData.FirstName;
            employee.LastName = newData.LastName;
            employee.Gender = newData._employee.Gender;
            employee.Birthdate.Day = newData.Day;
            employee.Birthdate.Month = newData.Month;
            employee.Birthdate.Year = newData.Year;
            employee.Email = newData.Email;
            employee.Phone = newData.Phone;
            employee.Pay = newData.Pay;
            employee.WorkTime = newData.WorkTime;
            employee.Address = newData.Address;
            //original.OnPropertyChanged("FirstName");
            original.OnPropertyChanged("DisplayName");
            original.OnPropertyChanged("Gender");
            original.OnPropertyChanged("Birthdate");
            original.OnPropertyChanged("Email");
            original.OnPropertyChanged("Phone");
            original.OnPropertyChanged("PrettyPay");
            original.OnPropertyChanged("WorkTime");
            original.OnPropertyChanged("Address");
            // TODO: Do a thorough testing of the app.
            //newData.CleanForm();
        }

        /// <summary>
        /// Enables _isEditingUser flag when only the edit modal dialog is activated.
        /// </summary>
        void Modal_Activated(object sender, EventArgs e)
        {
            _isEditingUser = true;
            EditModalOpen = true;
        }

        /// <summary>
        /// Disables _isEditingUser flag when the edit modal dialog is deactivated.
        /// </summary>
        void Modal_Deactivated(object sender, EventArgs e)
        {
            _isEditingUser = false;
        }

        /// <summary>
        /// Sets flag only when the modal is closed. That allow us to show only one modal at the time.
        /// </summary>
        void Modal_Closed(object sender, EventArgs e)
        {
            _isModalSpawned = false;
            EditModalOpen = false;
            OnPropertyChanged("DeleteToolTip");
        }

        /// <summary>
        /// Function that prints an Employee object.
        /// Just to make sure the data is being populated/edited correctly.
        /// </summary>
        /// <param name="item">Employee object.</param>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        void PrintEmployeeFields(Employee item)
        {
            Debug.Print(item.ToString());
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

                switch (propertyName)
                {
                    case "GenderType":
                        error = this.ValidateGenderType();
                        break;
                    case "WorkTimeType":
                        error = this.ValidateWorkTime();
                        break;
                    case "Day":
                        error = BirthDate.ValidateBirthdate(this.Day, this.Month, this.Year);
                        break;
                    default:
                        error = (_employee as IDataErrorInfo)[propertyName];
                        break;
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

        string ValidateWorkTime()
        {
            if (this.WorkTimeType == Resources.EmployeeWrapperViewModel_WorkingTimeOptions_FullTime
                || this.WorkTimeType == Resources.EmployeeWrapperViewModel_WorkingTimeOptions_PartTime)
            {
                return null;
            }

            return Resources.EmployeeWrapperViewModel_Error_MissingWorkTime;
        }

        #endregion // Interface Implementations

    }
}
