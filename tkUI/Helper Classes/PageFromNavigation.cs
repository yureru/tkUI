using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tkUI.Helper_Classes
{
    /// <summary>
    /// Class that should be implemented to create a page in a MVVM way, with the
    /// proper navigation button.
    /// </summary>
    abstract class PageFromNavigation : IPageViewModel, ISourceIcons
    {
        #region Fields

        bool _isChecked;

        #endregion

        #region Interface Implementations

        public abstract string Name { get; }

        public abstract string Clicked { get; }
        public abstract string Hover { get; }
        public abstract string Normal { get; }
        public abstract string Selected { get; }

        #endregion

        #region Properties

        /// <summary>
        /// This property is used for the IsChecked property of the Navigation's RadioButtons.
        /// The reason is that we need the first RadioButton to be IsChecked="True" by default
        /// since that's the view which is loaded.
        /// </summary>
        public bool Checked
        {
            get { return _isChecked; }
            set { _isChecked = value;  }
        }

        #endregion
    }
}
