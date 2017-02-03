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
    public partial class PageFlats : UserControl
    {
		public PageFlats()
        {
            DataContext = this;

            InitializeComponent();            

            context = new JKHModelContainer();

            updateListData();
            dgvTable.IsReadOnly = true;            
        }

        JKHModelContainer context;

        public Квартира SelectedFlat { get; set; }

        private void updateListData()
        {
            dgvTable.ItemsSource = context.КвартираSet.ToList();
        }

		private void btnAdd_Click(object sender, RoutedEventArgs e)
		{
            WindowAddFlat window = new WindowAddFlat();
            var result = window.ShowDialog();
            if (result ?? false)
            {
                updateListData();
            }
		}

		private void btnUpd_Click(object sender, RoutedEventArgs e)
		{
            if (SelectedFlat == null)
                return;

            WindowAddFlat window = new WindowAddFlat(SelectedFlat.Id);
            var result = window.ShowDialog();
            if(result ?? false)
            {
                context.Entry<Квартира>(SelectedFlat).Reload();
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
            if(SelectedFlat != null)
            {
                context.КвартираSet.Remove(SelectedFlat);
                context.SaveChanges();
                updateListData();
            }
        }
	}
}
