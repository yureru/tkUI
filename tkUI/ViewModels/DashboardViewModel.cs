using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using tkUI.Helper_Classes;

namespace tkUI.ViewModels
{
    class DashboardViewModel : ObservableObject, IPageViewModel, ISourceIcons
    {

        #region CLR Properties



        #endregion // CLR Properties

        #region Commands



        #endregion // Commands

        #region Interface Implementations
        public string Name
        {
            get
            {
                return "Dashboard";
            }
        }

        public string Clicked
        {
            get
            {
                return "";
            }
        }

        public string Hover
        {
            get
            {
                return "";
            }
        }


        public string Normal
        {
            get
            {
                return "Resources/IconsNavigation/DashboardIcon.png";
            }
        }

        public string Selected
        {
            get
            {
                return "Resources/IconsNavigation/DashboardSelectedIcon.png";
            }
        }

        #endregion // Interface Implementations


    }
}
