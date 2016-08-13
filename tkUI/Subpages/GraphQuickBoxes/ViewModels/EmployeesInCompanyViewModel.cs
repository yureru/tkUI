using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiveCharts;
using LiveCharts.Wpf;

using tkUI.Subpages.GraphQuickBoxes.Utils;

namespace tkUI.Subpages.GraphQuickBoxes.ViewModels
{
    class EmployeesInCompanyViewModel : PageFromQuickBoxes
    {

        #region Chart Variables

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        #endregion // Chart Variables

        #region Constructors

        public EmployeesInCompanyViewModel()
        {
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "2016",
                    Values = new ChartValues<double> { 8, 10, 12, 10, 13, 18, 14, 16, 19, 21, 18, 17 }
                }
            };

            /*SeriesCollection.Add(new ColumnSeries
            {
                Title = "2016",
                Values = new ChartValues<double> { 11, 56, 42 }
            });

            // also adding values updates and animates the chart automatically
            SeriesCollection[1].Values.Add(48d);*/

            Labels = new[] { "Ago", "Sep", "Oct", "Nov", "Dic", "Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul" };
            //Formatter = value => value.ToString("N");
        }

        #endregion // Constructors

        #region Interface Implementations

        public override string BoxName
        {
            get
            {
                return "Empleados en Webtasis";
            }
        }

        public override string Quantity
        {
            get
            {
                return "17"; // Dummy value
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion // Interface Implementations

    }
}
