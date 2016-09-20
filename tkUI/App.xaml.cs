using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using tkUI.Login;
using tkUI.Session;

namespace tkUI
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        static Window _loginWindow;
        static ApplicationView _app;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            // General
            /* TODO: Do the following tasks for Login
             * 1- The first window to spawn is gonna be the login.
             * 2- After the users enters the correct credentials (email and pasword) close the login window, make spawn the application
             * window, and make that new window the main one. 
             */

            // New Login window
            _loginWindow = new SessionView();
            SessionViewModel context = new SessionViewModel();
            _loginWindow.DataContext = context;
            _loginWindow.Show();
            //App.RunAppAfterSuccessfulLogin();

            // Old but login window
             /*
            _loginWindow = new LoginView();
            LoginViewModel context = new LoginViewModel();
            _loginWindow.DataContext = context;
            _loginWindow.Show();*/
            //App.RunAppAfterSuccessfulLogin();


            /*ApplicationView app = new ApplicationView();
            string path = "Data/employees.xml";
            ApplicationViewModel context = new ApplicationViewModel(path);
            app.DataContext = context;
            app.Show();*/
        }

        static public void RunAppAfterSuccessfulLogin()
        {
            _app = new ApplicationView();
            string path = "Data/employees.xml";
            ApplicationViewModel context = new ApplicationViewModel(path);
            _app.DataContext = context;
            _app.Show();

            _loginWindow.Close();
        }

    }
}
