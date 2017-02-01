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

        public WindowAddFlat(JKHModelContainer context)
        {            
            InitializeComponent();
            this.DataContext = this;
            
            _context = context;
            //var homes
            _flat = new Квартира();
            _flat.Дом = _context.ДомSet.ToList().First();            
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            _context.КвартираSet.Add(_flat);
            _context.SaveChanges();
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
    }
}