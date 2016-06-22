using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using tkUI.Helper_Classes;

namespace tkUI.ViewModels
{
    class PaymentsViewModel : PageFromNavigation
    {

        #region Interface Implementations
        public override string Name
        {
            get
            {
                return "Pagos";
            }
        }

        public override string Clicked
        {
            get
            {
                return "";
            }
        }

        public override string Hover
        {
            get
            {
                return "";
            }
        }


        public override string Normal
        {
            get
            {
                return "Resources/IconsNavigation/CreditCardIcon.png";
            }
        }

        public override string Selected
        {
            get
            {
                return "Resources/IconsNavigation/CreditCardSelectedIcon.png";
            }
        }

        #endregion // Interface Implementations

    }
}
