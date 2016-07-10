using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using LiveCharts.Wpf;
using System.Windows.Media.Animation;
using System.Windows.Markup;

using tkUI.Subpages.GraphQuickBoxes.ViewModels;

namespace tkUI.Subpages.GraphQuickBoxes.Views
{
    /// <summary>
    /// Lógica de interacción para ExpectedPaymentView.xaml
    /// </summary>
    public partial class ExpectedPaymentView : UserControl
    {

        ExpectedPaymentViewModel vm = new ExpectedPaymentViewModel();
        
        public ExpectedPaymentView()
        {

            InitializeComponent();

            CreateGraph();
        }

        // Creating the Chart in code behind to avoid this chart being created everytime we change the view.
        // wouldn't be a problem, but since the chart listens to new values, creating a new one makes the
        // old to be collected.
        void CreateGraph()
        {
            //!ELEMENT CartesianChart
            CartesianChart gr = new CartesianChart();
            gr.Name = "Chart";
            gr.DisableAnimations = true;
            gr.Series = vm.SeriesCollection;
            // Series Binding 
            /*Binding bndg = new Binding();
            // Next line don't know if neccessary 
            bndg.Source = vm;
            bndg.Path = new PropertyPath("SeriesCollection");
            gr.SetBinding(CartesianChart.SeriesProperty, bndg);*/
            gr.LegendLocation = LiveCharts.LegendLocation.Right;


            //!subELEMENT CartesianChart.AxisY
            // Works but commented
            Axis AxisY = new Axis();
            AxisY.Title = "Pagos";
            AxisY.LabelFormatter = vm.YFormatter;
           /* Binding bndgForm = new Binding();
            bndgForm.Source = vm;
            bndgForm.Path = new PropertyPath("YFormatter");
            AxisY.SetBinding(Axis.LabelFormatterProperty, bndgForm);*/
            //gr.AddToView(AxisY);
            gr.AxisY.Add(AxisY);


            //!subELEMENT CartesianChart.AxisX
            Axis AxisX = new Axis();
            AxisX.Title = "Mes";
            // This works
            AxisX.Labels = vm.Labels;
            /*Binding bndgLab = new Binding();
            bndgLab.Source = vm;
            bndgLab.Path = new PropertyPath("Labels");
            AxisX.SetBinding(Axis.LabelsProperty, bndgLab);*/
            /*gr.AddToView(AxisX);*/
            gr.AxisX.Add(AxisX);
            //gr.SetValue(Grid.RowProperty, 1);
            Grid.SetRow(gr, 1);
            Grid.SetColumn(gr, 0);
            GridContainer.Children.Add(gr);
        }
    }
}
