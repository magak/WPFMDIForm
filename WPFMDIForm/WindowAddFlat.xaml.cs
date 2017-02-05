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
        �������� _flat;

        public �������� Flat
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

        private List<���> _domSelection;

        public List<���> DomSelection
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

        public ����� SelectedDweller
        {
            get;
            set;
        }

        public List<�����> Dwellers
        {
            get
            {
                return _flat.�����.ToList();
            }
            set
            {
                _flat.����� = value;
                RaiseProprtyChanged("Dwellers");
            }
        }

        public ������� SelectedCounter
        {
            get;
            set;
        }

        public List<�������> Counters
        {
            get
            {
                return _flat.�������.ToList();
            }
            set
            {
                _flat.������� = value;
                RaiseProprtyChanged("Counters");
            }
        }

        private bool _editMode = false;

        public WindowAddFlat(int? flatId = null)
        {
            _context = new JKHModelContainer();

            if (flatId == null)
            {
                _flat = new ��������();
                _context.��������Set.Add(_flat);
            }
            else
            {
                _flat = _context.��������Set.Find(flatId);
                _editMode = true;
            }

            InitializeComponent();
            this.DataContext = this;

            DomSelection = _context.���Set.ToList();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _context.SaveChanges();
            }
            catch(Exception)
            {
                MessageBox.Show("��� ���� ������ ���� ��������� ���������", "������", MessageBoxButton.OK, MessageBoxImage.Error);
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
            _flat.�����.Remove(SelectedDweller);
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
                    _flat.�����.Add(item);
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
                    _flat.�������.Add(item);
                    RaiseProprtyChanged("Counters");
                }
            }
        }

        private void delCount_Click(object sender, RoutedEventArgs e)
        {
            _flat.�������.Remove(SelectedCounter);
            RaiseProprtyChanged("Counters");
        }
    }
}