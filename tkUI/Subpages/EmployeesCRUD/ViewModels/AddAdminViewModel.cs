﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using tkUI.Subpages.EmployeesCRUD.Utils;

namespace tkUI.Subpages.EmployeesCRUD.ViewModels
{
    class AddAdminViewModel : PageFromCRUD
    {

        #region Interface Implementations
        public override string Name
        {
            get
            {
                return "+ Añadir administrador";
            }
        }

        #endregion // Interface Implementations
    }
}
