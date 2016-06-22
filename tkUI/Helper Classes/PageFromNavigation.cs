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
    abstract class PageFromNavigation : ObservableObject, IPageViewModel, ISourceIcons
    {
        public abstract string Name { get; }

        public abstract string Clicked { get; }
        public abstract string Hover { get; }
        public abstract string Normal { get; }
        public abstract string Selected { get; }
    }
}
