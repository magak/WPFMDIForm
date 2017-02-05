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
    public partial class PageNorma : UserControl
    {
        public PageNorma()
        {
            DataContext = this;

            InitializeComponent();

            context = new JKHModelContainer();

            updateListData();
            dgvTable.IsReadOnly = true;
        }

        JKHModelContainer context;

        public Соц_норма SelectedNorma { get; set; }

        private void updateListData()
        {
            dgvTable.ItemsSource = context.Соц_нормаSet.ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            WindowAddNorma window = new WindowAddNorma();
            var result = window.ShowDialog();
            if (result ?? false)
            {
                updateListData();
            }
        }

        private void btnUpd_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedNorma == null)
                return;

            WindowAddNorma window = new WindowAddNorma(SelectedNorma.Id);
            var result = window.ShowDialog();
            if (result ?? false)
            {
                context.Entry<Соц_норма>(SelectedNorma).Reload();
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
            if (SelectedNorma != null)
            {
                context.Соц_нормаSet.Remove(SelectedNorma);
                context.SaveChanges();
                updateListData();
            }
        }
    }
}
