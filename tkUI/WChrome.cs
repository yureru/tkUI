using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Shell;

namespace tkUI
{
    /// <summary>
    /// Inherits from WindowChrome to set the Height of the Caption, since by default is aprox 28.
    /// </summary>
    public class WChrome : WindowChrome
    {
        public WChrome()
        {
            CaptionHeight = 14;
        }
    }
}
