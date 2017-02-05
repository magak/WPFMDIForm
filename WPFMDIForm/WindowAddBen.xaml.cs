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
    public partial class WindowAddBen : Window, INotifyPropertyChanged
    {
        JKHModelContainer _context;
        Льгота _ben;

        public Льгота Ben
        {
            get
            {
                return _ben;
            }
            set
            {
                _ben = value;
                RaiseProprtyChanged("Ben");
            }
        }

        public WindowAddBen(int? benId = null)
        {
            _context = new JKHModelContainer();

            if (benId == null)
            {
                _ben = new Льгота();
                _context.ЛьготаSet.Add(_ben);
            }
            else
            {
                _ben = _context.ЛьготаSet.Find(benId);
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