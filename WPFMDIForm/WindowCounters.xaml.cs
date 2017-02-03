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
    public class cntListItem
    {
        public cntListItem(bool selected, Счетчик counter)
        {
            _selected = selected;
            _counter = counter;
        }
        public bool _selected { get; set; }

        public Счетчик _counter { get; set; }
    }

    /// <summary>
    /// Interaction logic for WindowCounters.xaml
    /// </summary>
    public partial class WindowCounters : Window
    {
        private List<cntListItem> _counters;
        public List<cntListItem> Counters
        {
            get
            {
                return _counters;
            }
            set
            {
                Counters = value;
            }
        }

        JKHModelContainer _context;

        public WindowCounters(JKHModelContainer context)
        {
            DataContext = this;
            _context = context;
            var counters = _context.СчетчикSet.Where(r => r.Квартира == null).ToList();
            _counters = counters.Select(r => new cntListItem(false, r)).ToList();

            InitializeComponent();
        }

        public List<Счетчик> GetSelectedCounters()
        {
            return _counters.Where(s => s._selected).Select(s => s._counter).ToList();
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
