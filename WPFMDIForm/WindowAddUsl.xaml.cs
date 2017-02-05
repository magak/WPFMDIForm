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
    public partial class WindowAddUsl : Window, INotifyPropertyChanged
    {
        JKHModelContainer _context;
        Услуга _usl;

        public Услуга Usl
        {
            get
            {
                return _usl;
            }
            set
            {
                _usl = value;
                RaiseProprtyChanged("Usl");
            }
        }

        public WindowAddUsl(int? uslId = null)
        {
            _context = new JKHModelContainer();

            if (uslId == null)
            {
                _usl = new Услуга();
                _context.УслугаSet.Add(_usl);
            }
            else
            {
                _usl = _context.УслугаSet.Find(uslId);
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
    }
}