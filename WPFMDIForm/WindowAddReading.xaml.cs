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
    /// Interaction logic for Window7.xaml
    /// </summary>
    public partial class WindowAddReading : Window, INotifyPropertyChanged
    {
        JKHModelContainer _context;
        Показания_квартир _reading;

        public Показания_квартир Reading
        {
            get
            {
                return _reading;
            }
            set
            {
                _reading = value;
                RaiseProprtyChanged("Reading");
            }
        }

        private List<Счетчик> _counterSelection;

        public List<Счетчик> CounterSelection
        {
            get
            {
                return _counterSelection;
            }
            set
            {
                _counterSelection = value;
                RaiseProprtyChanged("CounterSelection");
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

        public WindowAddReading(int? readingId = null)
        {
            _context = new JKHModelContainer();

            if (readingId == null)
            {
                _reading = new Показания_квартир();
                _context.Показания_квартирSet.Add(_reading);
            }
            else
            {
                _reading = _context.Показания_квартирSet.Find(readingId);
                _editMode = true;
            }

            InitializeComponent();
            this.DataContext = this;

            CounterSelection = _context.СчетчикSet.Include("Квартира").Include("Квартира.Дом").ToList();
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
                this.DialogResult = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Все поля должны быть заполнены правильно", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                this.DialogResult = false;
            }
            this.Close();
        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();     
        }
    }
}
