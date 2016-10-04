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

        // Used to load/save xml elements and properties.
        static string[] _xmlElements = { "employees", "employee" };
        static string[] _xmlAttributes =
            {
                "id", "firstName", "lastName",
                "gender", "birthdate", "email",
                "phone", "pay", "workTime",
                "address", "startedWorking", "isAdmin",
                "currentlyEmployed"
            };

        static string[] _baseXMLOriginalPath;
        static string _baseXMLpath; // Path of where the .xml file is saved

        #endregion // Fields

        #region Constructors

        /// <summary>
        /// Creates a new repository of employees.
        /// </summary>
        /// <param name="employeeDataFile"></param>
        public EmployeeRepository(string employeeDataFile)
        {
            //_baseXMLOriginalPath = employeeDataFile;
            _employees = LoadEmployees(employeeDataFile);
            _currentID = GetLastID();
            _baseXMLpath = GoBackFolderPath(getPath(), '\\', 2);

            _baseXMLOriginalPath = employeeDataFile.Split('/');

            if (_baseXMLOriginalPath.Length != 2)
            {
                throw new ArgumentOutOfRangeException("XML Path isn't correct.");
            }
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
        /// <exception cref="ArgumentOutOfRangeException">"Inherited" from ExistsById function.</exception>
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

        public bool ExistsAtLeastOneAdmin()
        {
            bool foundAdmin = false;

            foreach (var employee in _employees)
            {
                if (employee.IsAdmin)
                {
                    foundAdmin = true;
                    break;
                }
            }

            return foundAdmin;
        }

        #endregion // Public Interface

        #region Private Helpers

        static List<Employee> LoadEmployees(string employeeDataFile)
        {
            ///TODO: Use a DB.
            using (Stream stream = GetResourceStream(employeeDataFile))
            using (XmlReader xmlRdr = new XmlTextReader(stream))
                return (from employeeElem in XDocument.Load(xmlRdr).Element(_xmlElements[0]).Elements(_xmlElements[1])
                        select Employee.CreateEmployee(
                            (int)employeeElem.Attribute(_xmlAttributes[0]),
                            (string)employeeElem.Attribute(_xmlAttributes[1]),
                            (string)employeeElem.Attribute(_xmlAttributes[2]),
                            (bool)employeeElem.Attribute(_xmlAttributes[3]),
                            (string)employeeElem.Attribute(_xmlAttributes[4]),
                            (string)employeeElem.Attribute(_xmlAttributes[5]),
                            (string)employeeElem.Attribute(_xmlAttributes[6]),
                            (string)employeeElem.Attribute(_xmlAttributes[7]),
                            (string)employeeElem.Attribute(_xmlAttributes[8]),
                            (string)employeeElem.Attribute(_xmlAttributes[9]),
                            (string)employeeElem.Attribute(_xmlAttributes[10]),
                            (bool)employeeElem.Attribute(_xmlAttributes[11]),
                            (bool)employeeElem.Attribute(_xmlAttributes[12]))).ToList();
        }

        /// <summary>
        /// Saves the employee collection by craeting a new temporal file, after this, it will
        /// overwrite the original one.
        /// </summary>
        public void SaveEmployees()
        {
            string newFilePath = _baseXMLpath + createTempXmlPath();
            Debug.Print(newFilePath);
            string originalXMLPath = _baseXMLpath + _baseXMLOriginalPath[0] + "/" + _baseXMLOriginalPath[1];
            Debug.Print(originalXMLPath);

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("  ");
            using (XmlWriter writer = XmlWriter.Create(newFilePath, settings))
            {
                writer.WriteStartElement(_xmlElements[0]); // Write root element

                // Write each element including properties.
                foreach (var employee in _employees)
                {
                    writer.WriteStartElement(_xmlElements[1]);
                    writer.WriteAttributeString(_xmlAttributes[0], employee.ID.ToString());
                    writer.WriteAttributeString(_xmlAttributes[1], employee.FirstName);
                    writer.WriteAttributeString(_xmlAttributes[2], employee.LastName);
                    writer.WriteAttributeString(_xmlAttributes[3], employee.Gender.ToString());
                    writer.WriteAttributeString(_xmlAttributes[4], (string)employee.Birthdate);
                    writer.WriteAttributeString(_xmlAttributes[5], employee.Email);
                    writer.WriteAttributeString(_xmlAttributes[6], employee.Phone);
                    writer.WriteAttributeString(_xmlAttributes[7], employee.Pay);
                    writer.WriteAttributeString(_xmlAttributes[8], employee.WorkTime);
                    writer.WriteAttributeString(_xmlAttributes[9], employee.Address);
                    writer.WriteAttributeString(_xmlAttributes[10], employee.StartedWorking);
                    writer.WriteAttributeString(_xmlAttributes[11], employee.IsAdmin.ToString());
                    writer.WriteAttributeString(_xmlAttributes[12], employee.CurrentlyEmployed.ToString());

                    writer.WriteEndElement();
                }
                writer.Flush();
            }

            OverwriteXML(newFilePath, originalXMLPath);

            // TODO: Check for exceptions.
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

        /// <summary>
        /// Creates a path (location) including the filename of the new temporal .xml document.
        /// </summary>
        /// <returns>The full path including filname.</returns>
        static string createTempXmlPath()
        {
            var path = _baseXMLOriginalPath[0] + "/temp" + createRandomId() + "_" + _baseXMLOriginalPath[1];
            return path;
        }

        static string createRandomId()
        {
            Random rand = new Random();
            int num = rand.Next(1, 1000);
            return num.ToString();
        }

        /// <summary>
        /// Serves to get the current location.
        /// </summary>
        /// <returns>Current location.</returns>
        static string getPath()
        {
            return Environment.CurrentDirectory;
        }

        /// <summary>
        /// Function that deletes ("goes back") from folders from a path. For example: If we specify a path like:
        /// "C:\User\Documents\Images\Vacations\" and we want that path to go back by two folders we will obtain:
        /// "C:\User\Documents\".
        /// </summary>
        /// <param name="originalPath">A path in the form of "folder/subFolder..."</param>
        /// <param name="slashType">The type of slash used to separate the path's folders.</param>
        /// <param name="howManyFoldersBack">Quantity of folders we're gonna go back.</param>
        /// <returns></returns>
        static string GoBackFolderPath(string originalPath, char slashType, int howManyFoldersBack)
        {
            if (originalPath == null)
            {
                throw new ArgumentNullException("Called GoBackFolderPath(null)");
            }

            char[] revChar = originalPath.ToCharArray();
            Array.Reverse(revChar);

            int index = 0, count = 0;

            for (int i = 0; i < revChar.Length; ++i)
            {
                if (revChar[i] == slashType)
                {
                    ++count;
                }

                if (count == howManyFoldersBack)
                {
                    index = i;
                    break;
                }
            }

            // Second if block (above) didn't execute.
            if (index == 0)
            {
                string times = howManyFoldersBack.ToString();
                throw new ArgumentException("Path couldn't go back " + times + "folders.");
            }

            var fixedChar = new string(revChar, index, revChar.Length - index);
            char[] foo = fixedChar.ToCharArray();
            Array.Reverse(foo);

            var fixedStr = new String(foo);
            return fixedStr;
        }

        /// <summary>
        /// Function that overwrite the original xml file.
        /// By "original" we meant the one that's being used to load the employee objects.
        /// </summary>
        /// <param name="newFile">File newly created.</param>
        /// <param name="originalFile">Original file to overwrite.</param>
        void OverwriteXML(string newFile, string originalFile)
        {
            // TODO: Handle exceptions, if exceptions occurs keep the original file. Delete newFile.
            File.Delete(originalFile);
            File.Move(newFile, originalFile);
        }

        /// <summary>
        /// Checks if an employee exists in with the given ID.
        /// </summary>
        /// <param name="id">Employee's ID. A non-zero, positive integer.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private bool ExistsByID(int id)
        {
            if (id <= 0)
            {
                var msg = String.Format("The employee can't exist since the ID ({0}) is equal or less than zero.", id);
                throw new ArgumentOutOfRangeException(msg);
            }

            var elem = (from item in _employees where String.Equals(item.ID, id) select item).ToList();

            if (elem.Count > 0)
            {
                return true;
            }

            return false;
        }

        #endregion // Private Helpers

    }
}
