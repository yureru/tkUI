using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tkUI.Session.Utils
{
    /// <summary>
    /// Properties for setting the Window's sizes properties.
    /// Note that the implementations might need not only need a get accessor,
    /// but a set too. This is due the size properties needs a TwoWay binding to work properly.
    /// </summary>
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
