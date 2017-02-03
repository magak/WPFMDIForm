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
    /// <summary>
    /// Interaction logic for Page6.xaml
    /// </summary>
    public partial class PageReadings : UserControl
    {
        public PageReadings()
        {
            DataContext = this;

            context = new JKHModelContainer();

            Calendars = new List<Календарь>();
            Calendars.Add(new Календарь());
            Calendars.AddRange(context.КалендарьSet);

            InitializeComponent();                        

            updateListData();
            dgvTable.IsReadOnly = true;
        }

        JKHModelContainer context;

        public List<Календарь> Calendars { get; set; }

        public Показания_квартир SelectedReading { get; set; }

        public Календарь _selectedCal;
        public Календарь SelectedCal
        {
            get { return _selectedCal; }
            set 
            { 
                _selectedCal = value;
                updateListData();
            }
        }

        private void updateListData()
        {
            if (SelectedCal ==null || string.IsNullOrEmpty(SelectedCal.Год) || string.IsNullOrEmpty(SelectedCal.Год))
            {
                dgvTable.ItemsSource = context.Показания_квартирSet.ToList();
            }
            else
            {
                dgvTable.ItemsSource = context.Показания_квартирSet.Where(s => s.Календарь.Id == SelectedCal.Id)
                    .ToList();
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedReading != null)
            {
                context.Показания_квартирSet.Remove(SelectedReading);
                context.SaveChanges();
                updateListData();
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            if (context != null)
                context.Dispose();
        }

        private void btnUpd_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedReading == null)
                return;

            WindowAddReading window = new WindowAddReading(SelectedReading.Id);
            var result = window.ShowDialog();
            if (result ?? false)
            {
                context.Entry<Показания_квартир>(SelectedReading).Reload();
                updateListData();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            WindowAddReading window = new WindowAddReading();
            var result = window.ShowDialog();
            if (result ?? false)
            {
                updateListData();
            }
        }
    }
}
