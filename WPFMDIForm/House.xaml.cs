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
    public partial class House : Window, INotifyPropertyChanged
    {
        JKHModelContainer _context;
        Дом _house;

        public Дом Dom
        {
            get
            {
                return _house;
            }
            set
            {
                _house = value;
                RaiseProprtyChanged("House");
            }
        }

        public House(int? houseId = null)
        {
            _context = new JKHModelContainer();

            if (houseId == null)
            {
                _house = new Дом();
                _context.ДомSet.Add(_house);
            }
            else
            {
                _house = _context.ДомSet.Find(houseId);
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

        private void del_Click(object sender, RoutedEventArgs e)
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