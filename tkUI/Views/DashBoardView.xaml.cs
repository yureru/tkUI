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

using tkUI.Subpages.GraphQuickBoxes.ViewModels;
using tkUI.Subpages.GraphQuickBoxes.Utils;
using tkUI.ViewModels;

namespace tkUI.Views
{
    /// <summary>
    /// Lógica de interacción para DashBoardView.xaml
    /// </summary>
    public partial class DashBoardView : UserControl
    {
        static ComboBox _oldSelector;
        static bool _wasBinded;
        public DashBoardView()
        {
            InitializeComponent();
            //GraphRange.ItemsSource = ComboboxesGraph.Items;
//            GraphRange.SelectedIndex = DashboardViewModel.CurrentSelectorDateRange.SelectedIndex;

            if (!_wasBinded)
            {
                bindCombobox();
                _wasBinded = true;
            }
            else
            {
                deleteBindCombobox();
                bindCombobox();
            }

            /*ExpectedPaymentViewModel context = new ExpectedPaymentViewModel();
            this.DataContext = context;*/
            
            
        }

        void bindCombobox()
        {
            // ItemsSource Binding
            Binding bindItems = new Binding();
            bindItems.Source = DashboardViewModel.CurrentSelectorDateRange;
            bindItems.Path = new PropertyPath("ItemsSource");
            GraphRange.SetBinding(ComboBox.ItemsSourceProperty, bindItems);

            // SelectedIndex Binding
            Binding bind = new Binding();
            bind.Source = DashboardViewModel.CurrentSelectorDateRange;
            bind.Path = new PropertyPath("SelectedIndex");
            GraphRange.SetBinding(ComboBox.SelectedIndexProperty, bind);
            _oldSelector = GraphRange;
        }

        void deleteBindCombobox()
        {
            BindingOperations.ClearAllBindings(_oldSelector);
        }

    }
}
