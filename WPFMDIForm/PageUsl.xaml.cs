using System;
using System.Collections.Generic;
using System.Data;
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
using WPFMDIForm.JKHModel;

namespace WPFMDIForm
{
    public partial class PageUsl : UserControl
    {
        public PageUsl()
        {
            DataContext = this;

            InitializeComponent();

            context = new JKHModelContainer();

            updateListData();
            dgvTable.IsReadOnly = true;
        }

        JKHModelContainer context;

        public Услуга SelectedUsl { get; set; }

        private void updateListData()
        {
            dgvTable.ItemsSource = context.УслугаSet.ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            WindowAddUsl window = new WindowAddUsl();
            var result = window.ShowDialog();
            if (result ?? false)
            {
                updateListData();
            }
        }

        private void btnUpd_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedUsl == null)
                return;

            WindowAddUsl window = new WindowAddUsl(SelectedUsl.Id);
            var result = window.ShowDialog();
            if (result ?? false)
            {
                context.Entry<Услуга>(SelectedUsl).Reload();
                updateListData();
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            if (context != null)
                context.Dispose();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedUsl != null)
            {
                context.УслугаSet.Remove(SelectedUsl);
                context.SaveChanges();
                updateListData();
            }
        }
    }
}
