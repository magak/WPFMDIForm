using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;
using WPFMDIForm.JKHModel;

namespace WPFMDIForm
{
    /// <summary>
    /// Interaction logic for Window6.xaml
    /// </summary>
    public partial class WindowAddODU : Window, INotifyPropertyChanged
    {
        JKHModelContainer _context;
        Показания_ОДУ _odu;

        public Показания_ОДУ Odu
        {
            get
            {
                return _odu;
            }
            set
            {
                _odu = value;
                RaiseProprtyChanged("Odu");
            }
        }

        private List<Дом> _homeSelection;

        public List<Дом> HomeSelection
        {
            get
            {
                return _homeSelection;
            }
            set
            {
                _homeSelection = value;
                RaiseProprtyChanged("HomeSelection");
            }
        }

        private List<Календарь> _calendarSelection;

        public List<Календарь> CalendarSelection
        {
            get
            {
                return _calendarSelection;
            }
            set
            {
                _calendarSelection = value;
                RaiseProprtyChanged("CalendarSelection");
            }
        }

        private bool _editMode = false;

        public WindowAddODU(int? oduId = null)
        {
            _context = new JKHModelContainer();

            if (oduId == null)
            {
                _odu = new Показания_ОДУ();
                _context.Показания_ОДУSet.Add(_odu);
            }
            else
            {
                _odu = _context.Показания_ОДУSet.Find(oduId);
                _editMode = true;
            }

            InitializeComponent();
            this.DataContext = this;

            HomeSelection = _context.ДомSet.ToList();
            CalendarSelection = _context.КалендарьSet.ToList();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaiseProprtyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (_context != null)
                _context.Dispose();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Все поля должны быть заполнены правильно", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            this.DialogResult = true;
            this.Close();
        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();     
        }
    }
}
