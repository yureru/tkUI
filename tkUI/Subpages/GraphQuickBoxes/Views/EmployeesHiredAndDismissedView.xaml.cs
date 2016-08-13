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

using tkUI.Subpages.GraphQuickBoxes.Utils;
using tkUI.Subpages.GraphQuickBoxes.ViewModels;

using LiveCharts;
using LiveCharts.Wpf;

namespace tkUI.Subpages.GraphQuickBoxes.Views
{
    /// <summary>
    /// Lógica de interacción para EmployeesHiredAndDismissedView.xaml
    /// </summary>
    public partial class EmployeesHiredAndDismissedView : UserControl, PersistentChart
    {

        #region Fields

        static CartesianChart _employeesHiredAndDismissedChart;
        static Grid _gContainer;
        static bool _wasInit;
        static EmployeesHiredAndDismissedViewModel _viewModel;

        #endregion // Fields

        #region Constructors

        public EmployeesHiredAndDismissedView()
        {
            InitializeComponent();

            if (!_wasInit)
            {
                _viewModel = new EmployeesHiredAndDismissedViewModel();
                ((PersistentChart)this).CreateGraph();
                _wasInit = true;
            } else
            {
                ((PersistentChart)this).AttachGraph();
            }
        }

        #endregion // Constructors

        void PersistentChart.AttachGraph()
        {
            _gContainer.Children.Remove(_employeesHiredAndDismissedChart);
            _gContainer = GridContainer; // Set to this newly instance created
            GridContainer.Children.Add(_employeesHiredAndDismissedChart);
        }

        void PersistentChart.CreateGraph()
        {
            _employeesHiredAndDismissedChart = new CartesianChart();
            _employeesHiredAndDismissedChart.Name = "Chart";
            _employeesHiredAndDismissedChart.Series = _viewModel.SeriesCollection;
            _employeesHiredAndDismissedChart.LegendLocation = LegendLocation.Left;

            Axis AxisX = new Axis();
            AxisX.Title = "Mes";
            AxisX.Labels = _viewModel.Labels;
            _employeesHiredAndDismissedChart.AxisX.Add(AxisX);

            Axis AxisY = new Axis();
            AxisY.Title = "Empleados";
            //AxisY.LabelFormatter = _viewModel.Formatter;

            LiveCharts.Wpf.Separator sep = new LiveCharts.Wpf.Separator();
            sep.IsEnabled = false;
            sep.Step = 1;
            AxisX.Separator = sep;

            _employeesHiredAndDismissedChart.AxisY.Add(AxisY);

            GridContainer.Children.Add(_employeesHiredAndDismissedChart);
            _gContainer = GridContainer;
        }
    }
}
