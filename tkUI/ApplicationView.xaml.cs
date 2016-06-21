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
using System.Windows.Shapes;

namespace tkUI
{
    /// <summary>
    /// Lógica de interacción para ApplicationView.xaml
    /// </summary>
    public partial class ApplicationView : Window
    {

        bool wasMoving;



        public ApplicationView()
        {
            InitializeComponent();
            // Use this if need WindowStyle="None"
            /*
            this.MaxHeight = SystemParameters.WorkArea.Height + 14;
            this.MaxWidth = SystemParameters.WorkArea.Width;*/
            // This for AllowTransparency="True"
            this.MaxHeight = SystemParameters.WorkArea.Height + 7;
            this.MaxWidth = SystemParameters.WorkArea.Width + 7;
        }

        /*private void DragWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
            DragWindow.Background = Brushes.Black;
        }

        private void DragBar_MouseDown(object sender, RoutedEventArgs e)
        {
            DragWindow.Background = Brushes.Black;
        }*/

        /*private void BorderDrag_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                
                if (BorderResize.Background == Brushes.AliceBlue)
                {
                    BorderResize.Background = Brushes.CornflowerBlue;
                }
                else
                {
                    BorderResize.Background = Brushes.AliceBlue;
                }
                if (WindowState == WindowState.Maximized) {
                    BorderResize_MouseMove(this, e);
                } else {
                    this.DragMove();
                }
                
            }
        }*/

        /* private void BorderResize_MouseMove(object sender, MouseEventArgs e)
         {*/
        /* if (BorderResize.Background == Brushes.AliceBlue)
         {
             BorderResize.Background = Brushes.CornflowerBlue;
         }
         else
         {
             BorderResize.Background = Brushes.AliceBlue;
         }*/
        /*if (WindowState == WindowState.Maximized && wasMaximized)
        {*/
        /* if (wasMoving)
         {
             WindowState = WindowState.Normal;
             this.DragMove();
             wasMoving = false;
         }*/
        /*}*/
        //}

        private void BorderResize_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                wasMoving = true;
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
