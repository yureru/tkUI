using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tkUI.Subpages.GraphQuickBoxes.Utils
{
    /// <summary>
    /// Interface to implement the code needed to create a Chart that mus persist
    /// between changing views. That allow us to improve performance a bit, since
    /// we aren't creating a chart everytime we change between views.
    /// </summary>
    interface PersistentChart
    {
        /// <summary>
        /// Creates a chart and adds it to the Grid.
        /// </summary>
        void CreateGraph();

        /// <summary>
        /// Attach the Chart to the current Grid.
        /// </summary>
        void AttachGraph();
    }
}
