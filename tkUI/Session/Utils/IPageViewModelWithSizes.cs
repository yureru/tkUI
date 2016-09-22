using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using tkUI.Helper_Classes;

namespace tkUI.Session.Utils
{
    /// <summary>
    /// Interface extension to avoid re-implement those in a monolothic one.
    /// </summary>
    interface IPageViewModelWithSizes : IWindowViewSizes, IPageViewModel
    {
    }
}
