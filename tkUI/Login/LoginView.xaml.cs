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

using System.Security;

namespace tkUI.Login
{
    /// <summary>
    /// Lógica de interacción para LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        static SecureString _pass;

        public LoginView()
        {
            InitializeComponent();
        }

        private void passwordTxt_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _pass = passwordTxt.SecurePassword;
        }

        public static SecureString Pass
        {
            get
            {
                return _pass;
            }
        }

    }
}
