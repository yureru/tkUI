using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using tkUI.Helper_Classes;

namespace tkUI.ViewModels
{
    class EmployeesViewModel : PageFromNavigation
    {

        #region Interface Implementations
        public override string Name
        {
            get
            {
                return "Empleados";
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
                return "Resources/IconsNavigation/HumanIcon.png";
            }
        }

        public override string Selected
        {
            get
            {
                return "Resources/IconsNavigation/HumanSelectedIcon.png";
            }
        }

        #endregion // Interface Implementations

    }
}
