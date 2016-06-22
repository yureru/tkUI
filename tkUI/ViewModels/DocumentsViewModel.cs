using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using tkUI.Helper_Classes;

namespace tkUI.ViewModels
{
    class DocumentsViewModel : PageFromNavigation
    {

        #region Interface Implementations
        public override string Name
        {
            get
            {
                return "Documentos";
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
                return "Resources/IconsNavigation/WrenchIcon.png";
            }
        }

        public override string Selected
        {
            get
            {
                return "Resources/IconsNavigation/WrenchSelectedIcon.png";
            }
        }

        #endregion // Interface Implementations

    }
}
