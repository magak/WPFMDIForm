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

namespace WPFMDIForm
{
    public partial class PageFlatData : UserControl
    {
		public PageFlatData()
        {
            InitializeComponent();

			DataTable table = new DataTable();
			table.BeginLoadData();
			table.Columns.Add("Номер", typeof(int));
			table.Columns.Add("Показания ГВС", typeof(float));
			table.Columns.Add("Показания ХВС", typeof(float));
			table.LoadDataRow(new object[] { 1, 128, 250 }, fAcceptChanges: true);
			table.LoadDataRow(new object[] { 3, 130, 259 }, fAcceptChanges: true);
			table.LoadDataRow(new object[] { 4, 269, 490 }, fAcceptChanges: true);
			table.EndLoadData();

			dgvTable.ItemsSource = table.DefaultView;
			dgvTable.IsReadOnly = true;
        }

		private void btnAdd_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
