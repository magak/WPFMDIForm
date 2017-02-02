using System;
using System.Collections.Generic;
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
    public class dwListItem
    {
        public dwListItem(bool selected, Жилец dweller)
        {
            _selected = selected;
            _dweller = dweller;
        }
        public bool _selected { get; set; }

        public Жилец _dweller { get; set; }
    }

    /// <summary>
    /// Interaction logic for WindowInhabits.xaml
    /// </summary>
    public partial class WindowInhabits : Window
    {
        private List<dwListItem> _dwellers;
        public List<dwListItem> Dwellers
        {
            get
            {
                return _dwellers;
            }
            set
            {
                Dwellers = value;
            }
        }

        JKHModelContainer _context;
        public WindowInhabits(JKHModelContainer context)
        {
            DataContext = this;
            _context = context;
            var dwellers = _context.ЖилецSet.Where(r => r.Квартира == null).ToList();
            _dwellers = dwellers.Select(r => new dwListItem(false, r)).ToList();

            InitializeComponent();
        }

        public List<Жилец> GetSelectedDwellers()
        {
            return _dwellers.Where(s => s._selected).Select(s=>s._dweller).ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnUpd_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
