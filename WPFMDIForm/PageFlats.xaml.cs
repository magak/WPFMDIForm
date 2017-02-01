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

            //DataTable table = new DataTable();
            //table.BeginLoadData();
            //table.Columns.Add("Номер", typeof(int));
            //table.Columns.Add("Площадь", typeof(float));
            //table.Columns.Add("Объем", typeof(float));
            //table.Columns.Add("ФИО проживающего", typeof(string));
            //table.Columns.Add("Счетчики установлены", typeof(bool));
            //table.Columns.Add("Число проживающих", typeof(int));
            //table.LoadDataRow(new object[] { 1, 50, 120, "И.И.Иванов", true, 1 }, fAcceptChanges: true);
            //table.LoadDataRow(new object[] { 3, 60, 140, "П.П.Петров", false, 3}, fAcceptChanges: true);
            //table.LoadDataRow(new object[] { 4, 60, 140, "С.С.Сидоров", false, 3 }, fAcceptChanges: true);
            //table.EndLoadData();

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
            WindowAddFlat window = new WindowAddFlat(context);
            //window.SelectedPhoto = (Photo)PhotosListBox.SelectedItem;
            var result = window.ShowDialog();
            if (result ?? false)
            {
                updateListData();
            }
		}

		private void btnUpd_Click(object sender, RoutedEventArgs e)
		{
            WindowAddFlat window = new WindowAddFlat(context);
			//window.SelectedPhoto = (Photo)PhotosListBox.SelectedItem;
            var result = window.ShowDialog();
            if(result ?? false)
            {
                updateListData();
            }
		}

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
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
