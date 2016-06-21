using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tkUI.Helper_Classes
{
    /// <summary>
    /// Path of an Icon's resource that a ModelView must implement to have a 
    /// navigation button.
    /// Notes:
    /// - To be used in conjunction with the ImageIcons class.
    /// - A class might not provide the path for every state.
    /// </summary>
    interface ISourceIcons
    {
        // Represents each UI State
        string Normal { get; }
        string Hover { get; }
        string Clicked { get; }
        string Selected { get; }
    }
}
