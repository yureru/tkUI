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

using System.Diagnostics;

using LiveCharts;
using LiveCharts.Wpf;

using tkUI.Subpages.GraphQuickBoxes.ViewModels;
using tkUI.Subpages.GraphQuickBoxes.Utils;

namespace tkUI.Subpages.GraphQuickBoxes.Views
{
    /// <summary>
    /// Lógica de interacción para EmployeesInCompanyView.xaml
    /// </summary>
    public partial class EmployeesInCompanyView : UserControl, PersistentChart
    {

        #region Fields

        static CartesianChart _employeesInCompanyChart;
        static Grid _gContainer;
        static bool _wasInit;
        static EmployeesInCompanyViewModel _viewModel;

        #endregion // Fields

        #region Constructors

        public EmployeesInCompanyView()
        {
            InitializeComponent();
            
            // Chart variables
            if (!_wasInit)
            {
                _viewModel = new EmployeesInCompanyViewModel();
                ((PersistentChart)this).CreateGraph();
                _wasInit = true;
            }
            else
            {
                ((PersistentChart)this).AttachGraph();
            }
        }

        #endregion // Constructors

        #region Interface Implementations

        void PersistentChart.CreateGraph()
        {
            _employeesInCompanyChart = new CartesianChart();
            _employeesInCompanyChart.Name = "Chart";
            _employeesInCompanyChart.Series = _viewModel.SeriesCollection;
            _employeesInCompanyChart.LegendLocation = LegendLocation.Left;

            Axis AxisX = new Axis();
            AxisX.Title = "Mes";
            AxisX.Labels = _viewModel.Labels;
            _employeesInCompanyChart.AxisX.Add(AxisX);

            Axis AxisY = new Axis();
            AxisY.Title = "Empleados";
            //AxisY.LabelFormatter = _viewModel.Formatter;

            LiveCharts.Wpf.Separator sep = new LiveCharts.Wpf.Separator();
            sep.IsEnabled = false;
            sep.Step = 1;
            AxisX.Separator = sep;

            _employeesInCompanyChart.AxisY.Add(AxisY);

            GridContainer.Children.Add(_employeesInCompanyChart);
            _gContainer = GridContainer;
        }

        void PersistentChart.AttachGraph()
        {
            _gContainer.Children.Remove(_employeesInCompanyChart);
            _gContainer = GridContainer; // Set to this newly instance created
            GridContainer.Children.Add(_employeesInCompanyChart);
        }

        #endregion // Interface Implementations
    }
}
