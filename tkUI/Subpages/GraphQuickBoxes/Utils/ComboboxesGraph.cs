using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tkUI.Subpages.GraphQuickBoxes.Utils
{
    static class ComboboxesGraph
    {
        static string[] _items;

        static ComboboxesGraph()
        {
            _items = new string[5];
            _items[0] = "Mes actual";
            _items[1] = "Últimos 2 meses";
            _items[2] = "Útlimos 4 meses";
            _items[3] = "Últimos 6 meses";
            _items[4] = "Año actual";
        }

        public static string[] Items
        {
            get { return _items; }
        }

    }
}
