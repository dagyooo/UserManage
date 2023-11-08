using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using UserManage.SQL;

namespace UserManage
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void StartUp(object sender, StartupEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();   
            mainWindow.Show();
        }
    }
}
