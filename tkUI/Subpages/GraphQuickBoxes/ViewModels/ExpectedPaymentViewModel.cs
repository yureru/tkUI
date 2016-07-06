using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using tkUI.Subpages.GraphQuickBoxes.Utils;

namespace tkUI.Subpages.GraphQuickBoxes.ViewModels
{
    class ExpectedPaymentViewModel : PageFromQuickBoxes
    {

        #region Interface Implementatios

        public override string BoxName
        {
            get
            {
                return "Cantidad total esperada a pagar";
            }
        }

        public override string Quantity
        {
            get
            {
                return "$ 234,987.00"; // Dummy value
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion // Interface Implementations
    }
}
