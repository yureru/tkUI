using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Diagnostics;

namespace tkUI.Models
{
    /// <summary>
    /// Represents an Employee of the company. This class
    /// has built-in validation logic.
    /// </summary>
    class Employee : IDataErrorInfo
    {

        #region Fields

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool Gender { get; set; }


        #endregion // Fields

        #region Creation

        public static Employee CreateNewEmployee()
        {
            return new Employee();
        }

        public static Employee CreateEmployee(string firstName, string lastName, bool gender)
        {
            return new Employee
            {
                FirstName = firstName,
                LastName = lastName,
                Gender = gender
            };
        }

        protected Employee()
        {
        }

        #endregion // Creation


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
            "LastName"
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

        static bool IsStringMissing(string value)
        {
            return String.IsNullOrEmpty(value) ||
                value.Trim() == String.Empty;
        }

        #endregion

    }
}
