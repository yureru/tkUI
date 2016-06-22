using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using tkUI.Helper_Classes;

namespace tkUI.ViewModels
{
    class SearchViewModel : PageFromNavigation
    {
        #region Interface Implementations
        public override string Name
        {
            get
            {
                return "Búsqueda";
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
                return "Resources/IconsNavigation/MagnifyingGlassIcon.png";
            }
        }

        public override string Selected
        {
            get
            {
                return "Resources/IconsNavigation/MagnifyingGlassSelectedIcon.png";
            }
        }

        #endregion // Interface Implementations

    }
}
