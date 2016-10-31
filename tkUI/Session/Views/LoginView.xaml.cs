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

using System.Security;

namespace tkUI.Session.Views
{
    /// <summary>
    /// Lógica de interacción para LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        #region Fields

        static SecureString _pass;
        public delegate void LoginFromPasswordBox();
        public static event LoginFromPasswordBox RequestLoginEvent; 

        #endregion // Fields

        public LoginView()
        {
            InitializeComponent();
        }

        private void passwordTxt_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _pass = passwordTxt.SecurePassword;
        }

        /// <summary>
        /// Exposes Password obtained from the passwordTxt textbox.
        /// </summary>
        public static SecureString Pass
        {
            get
            {
                return _pass;
            }
        }

        /// <summary>
        /// Handler to allow login from the PasswordBox when we hit enter.
        /// As you can see it's due the routed bubbling event (Preview).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoginView.RequestLoginEvent();
            }
        }
    }
}
