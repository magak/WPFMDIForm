using System;
using System.Windows;
using WPF.MDI;

namespace WPFMDIForm
{
   
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void menuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

		private void print_Click(object sender, RoutedEventArgs e)
		{
		}
		private void changeUser_Click(object sender, RoutedEventArgs e)
		{
			_openPage(new PageLogin());
		}
		private void flats_Click(object sender, RoutedEventArgs e)
		{
			_openPage(new PageFlats());
		}
		private void tariffs_Click(object sender, RoutedEventArgs e)
		{
			_openPage(new PageTariffs());
		}
		private void flatData_Click(object sender, RoutedEventArgs e)
		{
			_openPage(new PageFlatData());
		}
		private void oduData_Click(object sender, RoutedEventArgs e)
		{
			_openPage(new PageODU());
		}

		private void _openPage(UIElement content)
		{
			MainContainer.Content = content;
		}
    }
}
