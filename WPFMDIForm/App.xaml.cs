using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WPFMDIForm
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(string.Format("Ошибка: {0}{1}{2}", e.Exception.Message, Environment.NewLine, e.Exception.StackTrace)
                , "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }
    }
}
