using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using tkUI.Helper_Classes;

using tkUI.Properties;

namespace tkUI.Models
{
    /// <summary>
    /// Represents an Employee of the company. This class
    /// has built-in validation logic.
    /// </summary>
    class Employee : IDataErrorInfo
    {

        #region Fields

        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool Gender { get; set; }

        public Birth Birthdate { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Pay { get; set; }

        public string WorkTime { get; set; }

        public string Address { get; set; }

        public string StartedWorking { get; set; }

        #endregion // Fields


        /* Properties that aren't needed to save as part of Employee object
         * therefore they shouldn't be saved in a DB or in the repository. */
        #region Presentation Properties

        /// <summary>
        /// Used to know if an specific employee instance has a edit modal open,
        /// therefore it can't be deleted the Employee.
        /// </summary>
        public bool ModalOpen { get; set; }

        #endregion // Presentation Properties

        #region Creation

        public static Employee CreateNewEmployee()
        {
            return new Employee();
        }

        public static Employee CreateEmployee(int id, string firstName, string lastName, bool gender, string birthdate,
            string email, string phone, string pay, string workTime, string address, string startedWorking)
        {
            return new Employee
            {
                ID = id,
                FirstName = firstName,
                LastName = lastName,
                Gender = gender,
                Birthdate = (Birth)birthdate,
                Email = email,
                Phone = phone,
                Pay = pay,
                WorkTime = workTime,
                Address = address,
                StartedWorking = startedWorking
            };
        }

        protected Employee()
        {
        }

        /// <summary>
        /// Return a new employee based on an existing object.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public static Employee CreateEmployee(Employee employee)
        {
            return CreateEmployee(employee.ID, employee.FirstName, employee.LastName, employee.Gender, (string)employee.Birthdate,
            employee.Email, employee.Phone, employee.Pay, employee.WorkTime, employee.Address, Employee.StartedDate());
        }

        #endregion // Creation

        #region Helper Methods

        /// <summary>
        /// Returns date in the form "DD/MM/YYYY" at the time of Employee's creation (when started working.)
        /// </summary>
        /// <returns></returns>
        static string StartedDate()
        {
            var creationDate = DateTime.Today;
            return String.Format("{0}/{1}/{2}", creationDate.Day, creationDate.Month, creationDate.Year);
        }

        #endregion // Helper Methods

        #region Interface Implementations

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                return this.GetValidationError(propertyName);
            }
        }

        public string Error
        {
            get
            {
                return null;
            }
        }

        #endregion // Interface Implementations

        #region Validation

        /// <summary>
        /// Returns true if this object has no validation errors.
        /// </summary>
        public bool IsValid
        {
            get
            {
                foreach (string property in ValidatedProperties)
                {
                    if (GetValidationError(property) != null)
                        {
                            return false;
                        }
                }

                return true;
            }
        }

        static readonly string[] ValidatedProperties =
        {
            "FirstName",
            "LastName",
            "Email",
            "Phone",
            "Pay",
            "Address"
        };

        string GetValidationError(string propertyName)
        {
            if (Array.IndexOf(ValidatedProperties, propertyName) < 0)
            {
                return null;
            }

            string error = null;

            switch (propertyName)
            {
                case "FirstName":
                    error = this.ValidateFirstName();
                    break;
                case "LastName":
                    error = this.ValidateLastName();
                    break;
                case "Email":
                    error = this.ValidateEmail();
                    break;
                case "Phone":
                    error = this.ValidatePhone();
                    break;
                case "Pay":
                    error = this.ValidatePay();
                    break;
                case "Address":
                    error = this.ValidateAddress();
                    break;
                default:
                    Debug.Fail("Unexpected property being validated on Customer: " + propertyName);
                    break;
            }

            return error;
        }

        string ValidateFirstName()
        {
            if (IsStringMissing(this.FirstName))
            {
                return "Ingrese nombre";
            }
            return null;
        }

        string ValidateLastName()
        {
            if (IsStringMissing(this.LastName))
            {
                return "Ingrese apellido";
            }
            return null;
        }

        string ValidateEmail()
        {
            if (IsStringMissing(this.Email))
            {
                return Resources.Employee_Error_MissingEmail;
            }
            else if (!IsValidEmailAddress(this.Email))
            {
                return Resources.Employee_Error_InvalidEmail;
            }
            return null;
        }

        // TODO: Validate properly: Format, digits only, length, etc.
        string ValidatePhone()
        {
            if (IsStringMissing(this.Phone))
            {
                return Resources.Employee_Error_MissingPhone;
            }
            return null;
        }

        // TODO: Validate properly: Complying minimum wage, and not an exhorbitant salary.
        string ValidatePay()
        {
            if (IsStringMissing(this.Pay))
            {
                return Resources.Employee_Error_MissingPay;
            }
            return null;
        }

        string ValidateAddress()
        {
            if (IsStringMissing(this.Address))
            {
                return Resources.Employee_Error_MissingAddress;
            }
            return null;
        }

        static bool IsStringMissing(string value)
        {
            return String.IsNullOrEmpty(value) ||
                value.Trim() == String.Empty;
        }

        static bool IsValidEmailAddress(string email)
        {
            if (IsStringMissing(email))
                return false;

            // This regex pattern came from: http://haacked.com/archive/2007/08/21/i-knew-how-to-validate-an-email-address-until-i.aspx
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }

        #endregion // Validation

        #region Overrides

        /// <summary>
        /// Prints all the fields of Employee in a formatted manner.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return  "Name: " + this.FirstName + "\n"
                  + "SecondName: " + this.LastName + "\n"
                  + "ID: " + this.ID + "\n"
                  + "Gender: " + this.Gender + "\n"
                  + "Birth: " + this.Birthdate + "\n"
                  + "Email: " + this.Email + "\n"
                  + "Phone: " + this.Phone + "\n"
                  + "Pay: " + this.Pay + "\n"
                  + "Worktime: " + this.WorkTime + "\n"
                  + "Address: " + this.Address + "\n"
                  + "StartedWorking: " + this.StartedWorking + "\n";
        }

        #endregion // Overrides

    }
}
