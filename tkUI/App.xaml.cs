using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using tkUI.Login;

namespace tkUI
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            // General
            /* TODO: Do the following tasks for Login
             * 1- The first window to spawn is gonna be the login.
             * 2- After the users enters the correct credentials (email and pasword) close the login window, make spawn the application
             * window, and make that new window the main one. 
             */
            Window app = new LoginView();
            LoginViewModel context = new LoginViewModel();
            app.DataContext = context;
            app.Show();

            /*ApplicationView app = new ApplicationView();
            string path = "Data/employees.xml";
            ApplicationViewModel context = new ApplicationViewModel(path);
            app.DataContext = context;
            app.Show();*/
        }
    }
}
