using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using tkUI.Subpages.GraphQuickBoxes.Utils;

using LiveCharts;
using LiveCharts.Wpf;

namespace tkUI.Subpages.GraphQuickBoxes.ViewModels
{
    class EmployeesHiredAndDismissedViewModel : PageFromQuickBoxes
    {
        #region Chart Variables

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        #endregion // Chart Variables

        #region Constructors

        public EmployeesHiredAndDismissedViewModel()
        {
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Contratados",
                    Values = new ChartValues<double> { 8, 10, 12, 10, 13, 18, 14, 16, 19, 21, 18, 17 }
                }
            };

            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Despedidos",
                Values = new ChartValues<double> { 8, 10, 12, 10, 13, 18, 14, 16, 19, 21, 18, 17 }
            });

            Labels = new[] { "Ago", "Sep", "Oct", "Nov", "Dic", "Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul" };

        }

        #endregion // Constructors

        #region Interface Implementations

        public override string BoxName
        {
            get
            {
                return "Empleados contratados y despedidos";
            }
        }

        public override string Quantity
        {
            get
            {
                return "4";
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion // Interface Implementations
    }
}
