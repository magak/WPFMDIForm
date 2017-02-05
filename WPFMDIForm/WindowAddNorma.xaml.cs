using System;
using System.Windows;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using WPFMDIForm.JKHModel;
using System.ComponentModel;

namespace WPFMDIForm
{
    public partial class WindowAddNorma : Window, INotifyPropertyChanged
    {
        JKHModelContainer _context;
        Соц_норма _norma;

        public Соц_норма Norma
        {
            get
            {
                return _norma;
            }
            set
            {
                _norma = value;
                RaiseProprtyChanged("Norma");
            }
        }

        public WindowAddNorma(int? normaId = null)
        {
            _context = new JKHModelContainer();

            if (normaId == null)
            {
                _norma = new Соц_норма();
                _context.Соц_нормаSet.Add(_norma);
            }
            else
            {
                _norma = _context.Соц_нормаSet.Find(normaId);
            }

            InitializeComponent();
            this.DataContext = this;

            UslSelection = _context.УслугаSet.ToList();
        }

        private List<Услуга> _uslSelection;

        public List<Услуга> UslSelection
        {
            get
            {
                return _uslSelection;
            }
            set
            {
                _uslSelection = value;
                RaiseProprtyChanged("UslSelection");
            }
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

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
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
    }
}