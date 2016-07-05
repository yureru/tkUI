using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using tkUI.Helper_Classes;

namespace tkUI.ViewModels
{
    /*lass DashboardViewModel : ObservableObject, IPageViewModel, ISourceIcons*/
    class DashboardViewModel : PageFromNavigation
    {

        #region CLR Properties

        #endregion // CLR Properties

        #region Commands



        #endregion // Commands

        #region Interface Implementations
        public override string Name
        {
            get
            {
                return "Dashboard";
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
                return "Resources/IconsNavigation/DashboardIcon.png";
            }
        }

        public override string Selected
        {
            get
            {
                return "Resources/IconsNavigation/DashboardSelectedIcon.png";
            }
        }

        #endregion // Interface Implementations


    }
}
