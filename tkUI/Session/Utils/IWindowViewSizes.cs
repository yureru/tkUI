using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tkUI.Session.Utils
{
    interface IWindowViewSizes
    {
        string Height { get; }
        string Width { get; }

        string MinHeight { get; }
        string MinWidth { get; }

        string MaxHeight { get; }
        string MaxWidth { get; }
    }
}
