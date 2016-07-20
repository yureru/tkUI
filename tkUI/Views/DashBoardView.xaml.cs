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

namespace tkUI.Views
{
    /// <summary>
    /// Lógica de interacción para DashBoardView.xaml
    /// </summary>
    public partial class DashBoardView : UserControl
    {
        static PageFromQuickBoxes currentVM;
        static GraphComboboxes Comboboxes;

        public DashBoardView()
        {
            InitializeComponent();
            /*ExpectedPaymentViewModel context = new ExpectedPaymentViewModel();
            this.DataContext = context;*/
            ComboBox item = new ComboBox();
            string[] labels = new string[] { "First", "second", "Third" };
            item.ItemsSource = labels;
            //Wrapper = item;
            Grid.SetRow(item, 0);
            Main.Children.Add(item);
            // I think it would be bette create a MMVM for the Comboboxes itself, a ContentControl
            // that changes when the view is changed.
            currentVM = CurrentGraph.Content as PageFromQuickBoxes;
        }

        ///TODO: 1. Check if we're mapping correctly the ViewModels with the comboboxes // Sems Ok
        /// 2. Need a command to bind the action of the combobox with the corresponding chart
        /// 3. Check if when we change the QuickBox the combobox is changed too.
        /// 4. And also when changing from the navigation menu.
    }
}
