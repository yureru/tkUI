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
        // TODO: Do the following tasks
        /*
         * 2- Save the data of the current collection to a temporal file (employeesTempID.xaml for example) veryfing that
         * aren't any errors. OK.
         * 3- Save the collection to the xml file.
         * 4- Move/Overwrite to the original file.
             */

        #region Fields

        readonly List<Employee> _employees;

        int _currentID;

        static string[] _xmlElements = { "employees", "employee" };
        static string[] _xmlAttributes =
            {
                "id", "firstName", "lastName",
                "gender", "birthdate", "email",
                "phone", "pay", "workTime",
                "address", "startedWorking"
            };
        static string _xmlOriginalPath; // "Data/employees.xml"

        #endregion // Fields

        #region Constructors

        /// <summary>
        /// Creates a new repository of employees.
        /// </summary>
        /// <param name="employeeDataFile"></param>
        public EmployeeRepository(string employeeDataFile)
        {
            _xmlOriginalPath = employeeDataFile;
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

        public void DeleteByRange()
        {
            /*
             I think the current general steps would be:
             Click trash icon, Command gets the elements to delete, this data is passed to the EmployeeRepository
             which deletes the items.

            It would be *great* to pass only the data or the items that are selected to deletion, the easy way to solve this
            is use the IsSelected property, then loop through the all the elements (an O(n) operation smh), check for the IsSelected
            property, and pass a structure to the EmployeeRepository which will delete the items based on this data.
             */
            //var range = from emp in _employees where emp.IsSelected
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
                            (string)employeeElem.Attribute(_xmlAttributes[10]))).ToList();
        }

        public bool SaveEmployees()
        {

            string path = GoBackFolderPath(getPath(), '\\', 2) + createTempXmlPath();
            Debug.Print(path);

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("  ");
            using (XmlWriter writer = XmlWriter.Create(path, settings))
            {
                // Write XML data
                writer.WriteStartElement(_xmlElements[0]); // Write root element

                foreach (var employee in _employees)
                {
                    writer.WriteStartElement(_xmlElements[1]);
                    writer.WriteAttributeString(_xmlAttributes[0], employee.ID.ToString());
                    writer.WriteAttributeString(_xmlAttributes[1], employee.FirstName);
                    writer.WriteAttributeString(_xmlAttributes[2], employee.LastName);
                    writer.WriteAttributeString(_xmlAttributes[3], employee.);
                    writer.WriteAttributeString(_xmlAttributes[4], "true");
                    writer.WriteAttributeString(_xmlAttributes[5], "true");
                    writer.WriteAttributeString(_xmlAttributes[6], "true");
                    writer.WriteAttributeString(_xmlAttributes[7], "10");
                    writer.WriteAttributeString(_xmlAttributes[8], "true");
                    writer.WriteAttributeString(_xmlAttributes[9], "true");
                    writer.WriteAttributeString(_xmlAttributes[10], "true");
                    writer.WriteEndElement();
                }

                // Loop, writing all Fields of Employee of the collection.
                /*writer.WriteStartElement(_xmlElements[1]);
                writer.WriteAttributeString(_xmlAttributes[0], "1");
                writer.WriteAttributeString(_xmlAttributes[1], "Nachi");
                writer.WriteAttributeString(_xmlAttributes[2], "Sakaue");
                writer.WriteAttributeString(_xmlAttributes[3], "true");
                writer.WriteAttributeString(_xmlAttributes[4], "true");
                writer.WriteAttributeString(_xmlAttributes[5], "true");
                writer.WriteAttributeString(_xmlAttributes[6], "true");
                writer.WriteAttributeString(_xmlAttributes[7], "10");
                writer.WriteAttributeString(_xmlAttributes[8], "true");
                writer.WriteAttributeString(_xmlAttributes[9], "true");
                writer.WriteAttributeString(_xmlAttributes[10], "true");
                writer.WriteEndElement();
                writer.Flush();*/
            }

              

            return true; // TODO: Check for exceptions.
        }

        static Stream GetResourceStream(string resourceFile)
        {
            Uri uri = new Uri(resourceFile, UriKind.RelativeOrAbsolute);
            Debug.Print("GetResourceStream()");
            Debug.Print(uri.ToString());

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

        static string createTempXmlPath()
        {
            string[] splited = _xmlOriginalPath.Split('/');
            if (splited.Length != 2)
            {
                throw new ArgumentOutOfRangeException("XML Path isn't correct.");
            }
            //return "temp_" + splited[0] + createRandomId();
            //return splited[0] + "/temp" + createRandomId() + "_" +  splited[1];
            var path = splited[0] + "/temp" + createRandomId() + "_" + splited[1];
            Debug.Print(path);
            return path;
        }

        static string createRandomId()
        {
            Random rand = new Random();
            int num = rand.Next(1, 1000);
            return num.ToString();
        }

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

        #endregion // Private Helpers

    }
}
