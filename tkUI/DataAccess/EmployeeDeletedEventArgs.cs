using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tkUI.DataAccess
{
    /// <summary>
    /// Event used to pass what ID of an employee was requested to delete.
    /// </summary>
    class EmployeeDeletedEventArgs : EventArgs
    {
        public EmployeeDeletedEventArgs(int id)
        {
            this.ID = id;
        }

        public int ID { get; set; }
    }
}
