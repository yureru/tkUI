using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using tkUI.Subpages.EmployeesCRUD.Utils;

namespace tkUI.Subpages.EmployeesCRUD.ViewModel
{
    class AddEmployeeViewModel : PageFromCRUD
    {

        #region Interface Implemetations

        public override string Name
        {
            get
            {
                return "Añadir empleado";
            }
        }

        #endregion
    }
}
