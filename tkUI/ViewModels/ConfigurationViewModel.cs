using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using tkUI.Helper_Classes;

namespace tkUI.ViewModels
{
    class ConfigurationViewModel : PageFromNavigation
    {

        #region Interface Implementations
        public override string Name
        {
            get
            {
                return "Configuración";
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
                return "Resources/IconsNavigation/ClipBoardIcon.png";
            }
        }

        public override string Selected
        {
            get
            {
                return "Resources/IconsNavigation/ClipBoardSelectedIcon.png";
            }
        }

        #endregion // Interface Implementations

    }
}
