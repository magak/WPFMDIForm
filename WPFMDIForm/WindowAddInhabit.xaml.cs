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
    public partial class WindowAddInhabit : Window, INotifyPropertyChanged
    {
        JKHModelContainer _context;
        Жилец _inhabit;

        public Жилец Inhabit
        {
            get
            {
                return _inhabit;
            }
            set
            {
                _inhabit = value;
                RaiseProprtyChanged("Inhabit");
            }
        }

        public Льгота SelectedLgota
        {
            get;
            set;
        }

        public List<Льгота> Lgoty
        {
            get
            {
                return _inhabit.Льгота.ToList();
            }
            set
            {
                _inhabit.Льгота = value;
                RaiseProprtyChanged("Lgoty");
            }
        }

        public WindowAddInhabit(int? inhabitId = null)
        {
            _context = new JKHModelContainer();

            if (inhabitId == null)
            {
                _inhabit = new Жилец();
                _context.ЖилецSet.Add(_inhabit);
            }
            else
            {
                _inhabit = _context.ЖилецSet.Find(inhabitId);
            }

            InitializeComponent();
            this.DataContext = this;
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

        private void addLgota_Click(object sender, RoutedEventArgs e)
        {
            WindowLgoty window = new WindowLgoty(_context);
            var result = window.ShowDialog();
            if (result ?? false)
            {
                foreach (var item in window.GetSelectedLgoty())
                {
                    _inhabit.Льгота.Add(item);
                    RaiseProprtyChanged("Lgoty");
                }
            }
        }

        private void delLgota_Click(object sender, RoutedEventArgs e)
        {
            _inhabit.Льгота.Remove(SelectedLgota);
            RaiseProprtyChanged("Lgoty");
        }
    }
}