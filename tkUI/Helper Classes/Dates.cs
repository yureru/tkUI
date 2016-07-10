using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tkUI.Helper_Classes
{

    struct Dates
    {
        public enum Month { Enero, Febrero, Marzo, Abril,
                            Mayo, Junio, Julio, Agosto,
                            Septiembre, Octubre, Noviembre, Diciembre };

        public Month Value { get; set; }

        public Dates(Month month)
        {
            Value = month;
        }

        public string Abbreviation()
        {
            switch (Value)
            {
                case Month.Enero: return "Ene";
                case Month.Febrero: return "Feb";
                case Month.Marzo: return "Mar";
                case Month.Abril: return "Abr";
                case Month.Mayo: return "May";
                case Month.Junio: return "Jun";
                case Month.Julio: return "Jul";
                case Month.Agosto: return "Ago";
                case Month.Septiembre: return "Sep";
                case Month.Octubre: return "Oct";
                case Month.Noviembre: return "Nov";
                case Month.Diciembre: return "Dic";
                default: return "";
            }
        }


    }
}
