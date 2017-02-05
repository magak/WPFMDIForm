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
    public partial class WindowAddFlat : Window, INotifyPropertyChanged
    {
        JKHModelContainer _context;
        Квартира _flat;

        public Квартира Flat
        {
            get
            {
                return _flat; 
            }
            set 
            {
                _flat = value;
                RaiseProprtyChanged("Flat");
            }
        }

        private List<Дом> _domSelection;

        public List<Дом> DomSelection
        {
            get
            {
                return _domSelection;
            }
            set
            {
                _domSelection = value;
                RaiseProprtyChanged("DomSelection");
            }
        }

        public Жилец SelectedDweller
        {
            get;
            set;
        }

        public List<Жилец> Dwellers
        {
            get
            {
                return _flat.Жилец.ToList();
            }
            set
            {
                _flat.Жилец = value;
                RaiseProprtyChanged("Dwellers");
            }
        }

        public Счетчик SelectedCounter
        {
            get;
            set;
        }

        public List<Счетчик> Counters
        {
            get
            {
                return _flat.Счетчик.ToList();
            }
            set
            {
                _flat.Счетчик = value;
                RaiseProprtyChanged("Counters");
            }
        }

        private bool _editMode = false;

        public WindowAddFlat(int? flatId = null)
        {
            _context = new JKHModelContainer();

            if (flatId == null)
            {
                _flat = new Квартира();
                _context.КвартираSet.Add(_flat);
            }
            else
            {
                _flat = _context.КвартираSet.Find(flatId);
                _editMode = true;
            }

            InitializeComponent();
            this.DataContext = this;

            DomSelection = _context.ДомSet.ToList();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _context.SaveChanges();
            }
            catch(Exception)
            {
                MessageBox.Show("Все поля должны быть заполнены правильно", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                this.DialogResult = false;
                this.Close();
            }
            this.DialogResult = true;
            this.Close();
        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();     
        }

        /*private void Rotate(object sender, RoutedEventArgs e)
        {

            BitmapSource img = (BitmapSource)(_photo.Image);

            CachedBitmap cache = new CachedBitmap(img, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            _photo.Image = BitmapFrame.Create(new TransformedBitmap(cache, new RotateTransform(90.0)));

            ViewedPhoto.Source = _photo.Image;
        }*/

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

        private void delRom_Click(object sender, RoutedEventArgs e)
        {
            _flat.Жилец.Remove(SelectedDweller);
            RaiseProprtyChanged("Dwellers");
        }

        private void addRom_Click(object sender, RoutedEventArgs e)
        {
            WindowInhabits window = new WindowInhabits(_context);
            var result = window.ShowDialog();
            if(result ?? false)
            {
                foreach(var item in window.GetSelectedDwellers())
                {
                    _flat.Жилец.Add(item);
                    RaiseProprtyChanged("Dwellers");
                }
            }
        }

        private void addCount_Click(object sender, RoutedEventArgs e)
        {
            WindowCounters window = new WindowCounters(_context);
            var result = window.ShowDialog();
            if (result ?? false)
            {
                foreach (var item in window.GetSelectedCounters())
                {
                    _flat.Счетчик.Add(item);
                    RaiseProprtyChanged("Counters");
                }
            }
        }

        private void delCount_Click(object sender, RoutedEventArgs e)
        {
            _flat.Счетчик.Remove(SelectedCounter);
            RaiseProprtyChanged("Counters");
        }
    }
}