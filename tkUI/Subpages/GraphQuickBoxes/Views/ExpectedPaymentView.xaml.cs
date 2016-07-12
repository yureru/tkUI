﻿using System;
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

        #region Fields
        static CartesianChart _paymentsChart;
        static Grid _gContainer; // Allows to remove the chart from the old Grid's

        static bool _wasInit;

        static ExpectedPaymentViewModel _viewModel;

        #endregion // Fields

        #region Contructors

        public ExpectedPaymentView()
        {
            InitializeComponent();

            if (!_wasInit)
            {
                _viewModel = new ExpectedPaymentViewModel();
                CreateGraph();
                _wasInit = true;
            }
            else
            {
                _gContainer.Children.Remove(_paymentsChart);
                _gContainer = GridContainer; // Set to this newly instance created
                Grid.SetRow(_paymentsChart, 1);
                Grid.SetColumn(_paymentsChart, 0);
                GridContainer.Children.Add(_paymentsChart);
            }
        }

        #endregion // Constructors

        #region Properties

        public string DateRangeItem
        {
            get { return _viewModel.CurrentItemCombobox; }
            set { _viewModel.CurrentItemCombobox = value; }
        }

        #endregion // Properties

        #region Methods

        /// <summary>
        /// Creates a chart and adds it to the Grid.
        /// </summary>
        void CreateGraph()
        {
            //!ELEMENT CartesianChart
            _paymentsChart = new CartesianChart();
            _paymentsChart.Name = "Chart";
            //gr.DisableAnimations = true;
            _paymentsChart.Series = _viewModel.SeriesCollection;
            // Series Binding 
            /*Binding bndg = new Binding();
            // Next line don't know if neccessary 
            bndg.Source = _viewModel;
            bndg.Path = new PropertyPath("SeriesCollection");
            gr.SetBinding(CartesianChart.SeriesProperty, bndg);*/
            _paymentsChart.LegendLocation = LiveCharts.LegendLocation.Right;


            //!subELEMENT CartesianChart.AxisY
            // Works but commented
            Axis AxisY = new Axis();
            AxisY.Title = "Pagos";
            AxisY.LabelFormatter = _viewModel.YFormatter;
            /* Binding bndgForm = new Binding();
             bndgForm.Source = _viewModel;
             bndgForm.Path = new PropertyPath("YFormatter");
             AxisY.SetBinding(Axis.LabelFormatterProperty, bndgForm);*/
            //gr.AddToView(AxisY);
            _paymentsChart.AxisY.Add(AxisY);


            //!subELEMENT CartesianChart.AxisX
            Axis AxisX = new Axis();
            AxisX.Title = "Mes";
            // This works
            AxisX.Labels = _viewModel.Labels;
            /*Binding bndgLab = new Binding();
            bndgLab.Source = _viewModel;
            bndgLab.Path = new PropertyPath("Labels");
            AxisX.SetBinding(Axis.LabelsProperty, bndgLab);*/
            /*gr.AddToView(AxisX);*/
            // Separator 
            LiveCharts.Wpf.Separator sep = new LiveCharts.Wpf.Separator();
            sep.IsEnabled = false;
            sep.Step = 1;
            AxisX.Separator = sep; // Add separator

            _paymentsChart.AxisX.Add(AxisX);
            //gr.SetValue(Grid.RowProperty, 1);
            Grid.SetRow(_paymentsChart, 1);
            Grid.SetColumn(_paymentsChart, 0);
            GridContainer.Children.Add(_paymentsChart);
            _gContainer = GridContainer; // Save the Grid's reference to remove the chart later
        }

        #endregion // Methods
    }
}
