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
using WPFMDIForm.JKHModel;

namespace WPFMDIForm
{
    public partial class PageODU : UserControl
    {
        public PageODU()
        {
            DataContext = this;

            context = new JKHModelContainer();

            InitializeComponent();

            updateListData();
            dgvTable.IsReadOnly = true;
        }

        JKHModelContainer context;

        public Показания_ОДУ SelectedOdu { get; set; }

        private void updateListData()
        {
            dgvTable.ItemsSource = context.Показания_ОДУSet.Include("Календарь").Include("Дом").ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            WindowAddODU window = new WindowAddODU();
            var result = window.ShowDialog();
            if (result ?? false)
            {
                updateListData();
            }
        }

        private void btnUpd_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedOdu == null)
                return;

            WindowAddODU window = new WindowAddODU(SelectedOdu.Id);
            var result = window.ShowDialog();
            if (result ?? false)
            {
                context.Entry<Показания_ОДУ>(SelectedOdu).Reload();
                updateListData();
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedOdu != null)
            {
                context.Показания_ОДУSet.Remove(SelectedOdu);
                context.SaveChanges();
                updateListData();
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            if (context != null)
                context.Dispose();
        }
    }
}
