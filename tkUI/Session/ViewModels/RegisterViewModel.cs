using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using tkUI.Helper_Classes;
using tkUI.Session.Utils;

namespace tkUI.Session.ViewModels
{
    class RegisterViewModel : IPageViewModelWithSizes
    {

        #region Fields

        Action<RequestedViewToGO> _changeViewModelManually;

        #endregion // Fields

        #region Constructors

        public RegisterViewModel(Action<RequestedViewToGO> changeViewModelManually)
        {
            _changeViewModelManually = changeViewModelManually;
        }

        

        #endregion // Constructors

        #region Interface Implementations

        public string Name
        {
            get
            {
                return "Registrar Administrador - timekeeping";
            }
        }

        public string Height
        {
            get
            {
                return "200";
            }

            set { }
        }

        public string Width
        {
            get
            {
                return "150";
            }

            set { }
        }

        public string MinHeight
        {
            get
            {
                return Height;
            }
        }

        public string MinWidth
        {
            get
            {
                return Width;
            }
        }

        public string MaxHeight
        {
            get
            {
                return "300";
            }
            set { }
        }

        public string MaxWidth
        {
            get
            {
                return "250";
            }
            set { }
        }



        #endregion // Interface Implementations

    }
}
