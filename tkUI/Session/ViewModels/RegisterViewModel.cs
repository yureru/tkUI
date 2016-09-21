using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using tkUI.Helper_Classes;

namespace tkUI.Session.ViewModels
{
    class RegisterViewModel : IPageViewModel
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

        #endregion // Interface Implementations

    }
}
