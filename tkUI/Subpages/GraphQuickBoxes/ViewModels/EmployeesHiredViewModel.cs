using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using tkUI.Subpages.GraphQuickBoxes.Utils;

namespace tkUI.Subpages.GraphQuickBoxes.ViewModels
{
    class EmployeesHiredViewModel : PageFromQuickBoxes
    {

        #region Interface Implementatios

        public override string BoxName
        {
            get
            {
                return "Contratados este mes";
            }
        }

        public override string Quantity
        {
            get
            {
                return "3"; // Dummy value
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion // Interface Implementations

    }
}
