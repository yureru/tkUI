using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using tkUI.Subpages.GraphQuickBoxes.Utils;

namespace tkUI.Subpages.GraphQuickBoxes.ViewModels
{
    class EmployeesInCompanyViewModel : PageFromQuickBoxes
    {

        #region Interface Implementatios

        public override string BoxName
        {
            get
            {
                return "Empleados en Webtasis";
            }
        }

        public override string Quantity
        {
            get
            {
                return "17"; // Dummy value
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion // Interface Implementations

    }
}
