using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using tkUI.Helper_Classes;
using tkUI.Subpages.GraphQuickBoxes.Utils;
using tkUI.Subpages.GraphQuickBoxes.Models;


using LiveCharts;
using LiveCharts.Wpf;

namespace tkUI.Subpages.GraphQuickBoxes.ViewModels
{
    class ExpectedPaymentViewModel : PageFromQuickBoxes
    {

        #region Fields

        ExpectedPaymentModel model;
 
        static SeriesCollection graph;

        // Chart variables
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        #endregion



        public ExpectedPaymentViewModel()
        {
            model = new ExpectedPaymentModel();

            // Pollute the data, should get this from a DB
            model.Payments.Add(151000);
            model.Payments.Add(110000);
            model.Payments.Add(120000);
            model.Payments.Add(170000);
            model.Payments.Add(285000);
            model.Payments.Add(295000);
            model.Payments.Add(235000);
            model.Payments.Add(305000);
            model.Payments.Add(275000);
            model.Payments.Add(160000);
            model.Payments.Add(110000);
            model.Payments.Add(234987);


            var valTemp = new ChartValues<double>();

            foreach (var i in model.Payments)
            {
                valTemp.Add(i);
            }

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Cantidad pagada",
                    Values = valTemp
                }
            };
            
            /*for (Dates.Month month = Dates.Month.Enero; month < Dates.Month.Diciembre; ++month)
            {
                model.Months.Add(new Dates(month));
            }

            Labels */

            Labels = new[] { "Ago", "Sep", "Oct", "Nov", "Dic", "Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul" };
            YFormatter = value => value.ToString("C");

            graph = SeriesCollection;
        }

        #region Interface Implementations

        public override string BoxName
        {
            get
            {
                return "Cantidad total esperada a pagar";
            }
        }

        public override string Quantity
        {
            get
            {
                return "$ 234,987.00"; // Dummy value
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion // Interface Implementations
    }
}
