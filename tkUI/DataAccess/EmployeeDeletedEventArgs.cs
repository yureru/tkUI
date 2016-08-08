using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tkUI.DataAccess
{
    class EmployeeDeletedEventArgs : EventArgs
    {

        public EmployeeDeletedEventArgs(int id)
        {
            this.ID = id;
        }

        public int ID { get; set; }

    }
}
