using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using tkUI.Models;

namespace tkUI.DataAccess
{
    /// <summary>
    /// Event arguments used by CustomerRepository's CustomerAdded event.
    /// </summary>
    class EmployeeAddedEventArgs : EventArgs
    {
        public EmployeeAddedEventArgs(Employee newEmployee)
        {
            this.NewEmployee = newEmployee;
        }

        public Employee NewEmployee { get; private set; }
    }
}
