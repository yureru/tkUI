﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using tkUI.Helper_Classes;
using tkUI.Session.Utils;
using tkUI.Properties;

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
                return Resources.Session_RegisterViewModel_WindowTitle + Resources.App_Name;
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

            set { }
        }

        public string MinWidth
        {
            get
            {
                return Width;
            }

            set { }
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
