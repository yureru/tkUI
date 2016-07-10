using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using tkUI.Subpages.GraphQuickBoxes.Utils;

namespace tkUI.Subpages.GraphQuickBoxes.ViewModels
{
    class EmployeesDismissedViewModel : PageFromQuickBoxes
    {
        #region Interface Implementatios

        public override string BoxName
        {
            get
            {
                return "Despedidos este mes";
            }
        }

        public override string Quantity
        {
            get
            {
                return "0"; // Dummy value
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion // Interface Implementations

    }
}
