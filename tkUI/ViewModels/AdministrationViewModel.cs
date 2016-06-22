using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using tkUI.Helper_Classes;

namespace tkUI.ViewModels
{
    class AdministrationViewModel : PageFromNavigation
    {

        #region Interface Implementations
        public override string Name
        {
            get
            {
                return "Administración";
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
                return "Resources/IconsNavigation/PersonalIDIcon.png";
            }
        }

        public override string Selected
        {
            get
            {
                return "Resources/IconsNavigation/PersonalIDSelectedIcon.png";
            }
        }

        #endregion // Interface Implementations

    }
}
