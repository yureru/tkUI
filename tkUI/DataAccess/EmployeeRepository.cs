using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Windows;
using System.Windows.Resources;

using tkUI.Models;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Collections.ObjectModel;


namespace tkUI.DataAccess
{
    /// <summary>
    /// Represents a source of employees in the application.
    /// </summary>
    class EmployeeRepository
    {

        #region Fields

        readonly List<Employee> _employees;

        int _currentID;

        #endregion // Fields

        #region Constructors

        /// <summary>
        /// Creates a new repository of employees.
        /// </summary>
        /// <param name="employeeDataFile"></param>
        public EmployeeRepository(string employeeDataFile)
        {
            _employees = LoadEmployees(employeeDataFile);
            _currentID = GetLastID();
        }

        #endregion // Constructors

        #region Public Interface

        /// <summary>
        /// Raised when a employee is placed into the repository.
        /// </summary>
        public event EventHandler<EmployeeAddedEventArgs> EmployeeAdded;

        /// <summary>
        /// Raised when a employee is asked to be deleted.
        /// </summary>
        public event EventHandler<EmployeeDeletedEventArgs> EmployeeDeleted;

        /// <summary>
        /// Places the specified customer into the repository.
        /// If the customer is already in the repository, an
        /// exception is not thrown.
        /// </summary>
        /// <param name="employee"></param>
        public void AddEmployee(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException("employee");
            }

            if (!_employees.Contains(employee))
            {
                // Set locally to last ID
                employee.ID = GetID;
                _employees.Add(employee);

                if (this.EmployeeAdded != null)
                {
                    this.EmployeeAdded(this, new EmployeeAddedEventArgs(employee));
                }
            }
        }

        /// <summary>
        /// Returns true if the specified employee exists in the
        /// repository, or false if it is not.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public bool ContainsEmployee(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException("employee");
            }

            return _employees.Contains(employee);
        }

        /// <summary>
        /// Returns a shallow-copied list of all employees in the repository.
        /// </summary>
        /// <returns></returns>
        public List<Employee> GetEmployees()
        {
            return new List<Employee>(_employees);
        }

        /// <summary>
        /// Search for a given ID, if it's found removes the employeee
        /// containing that ID. It raises the EmployeeDeletedEventArgs.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteByID(int id)
        {
            if (ExistsByID(id))
            {
                for (int i = 0; i < _employees.Count; ++i)
                {
                    if (_employees[i].ID == id)
                    {
                        _employees.RemoveAt(i);
                        if (this.EmployeeDeleted != null)
                        {
                            this.EmployeeDeleted(this, new EmployeeDeletedEventArgs(id));
                        }
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Checks if an employee exists in with the given ID.
        /// </summary>
        /// <param name="id">Employee's ID. A non-zero, positive integer.</param>
        /// <returns></returns>
        public bool ExistsByID(int id)
        {
            var elem = (from item in _employees where String.Equals(item.ID, id) select item).ToList();

            if (elem.Count > 0)
            {
                return true;
            }

            return false;
        }

        #endregion // Public Interface

        #region Private Helpers

        static List<Employee> LoadEmployees(string employeeDataFile)
        {
            ///TODO: Use a DB.
            using (Stream stream = GetResourceStream(employeeDataFile))
            using (XmlReader xmlRdr = new XmlTextReader(stream))
                return (from employeeElem in XDocument.Load(xmlRdr).Element("employees").Elements("employee")
                        select Employee.CreateEmployee(
                            (int)employeeElem.Attribute("id"),
                            (string)employeeElem.Attribute("firstName"),
                            (string)employeeElem.Attribute("lastName"),
                            (bool)employeeElem.Attribute("gender"))).ToList();
        }

        static Stream GetResourceStream(string resourceFile)
        {
            Uri uri = new Uri(resourceFile, UriKind.RelativeOrAbsolute);
            
            StreamResourceInfo info = Application.GetResourceStream(uri);
            if (info == null || info.Stream == null)
            {
                throw new ApplicationException("Missing resource file " + resourceFile);
            }

            return info.Stream;
        }
        
        int GetID
        {
            get { return ++_currentID; }
        }

        /// <summary>
        /// Returns the greater ID of the list of employees.
        /// </summary>
        /// <returns></returns>
        int GetLastID()
        {
            return _employees.Max(emp => emp.ID);
        }

        #endregion // Private Helpers

    }
}
