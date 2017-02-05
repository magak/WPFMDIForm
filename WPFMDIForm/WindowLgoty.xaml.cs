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
    public class LgotyListItem
    {
        public LgotyListItem(bool selected, Льгота lgota)
        {
            _selected = selected;
            _lgota = lgota;
        }
        public bool _selected { get; set; }

        public Льгота _lgota { get; set; }
    }

    /// <summary>
    /// Interaction logic for WindowLgoty.xaml
    /// </summary>
    public partial class WindowLgoty : Window
    {
        private List<LgotyListItem> _lgoty;
        public List<LgotyListItem> Lgoty
        {
            get
            {
                return _lgoty;
            }
            set
            {
                Lgoty = value;
            }
        }

        JKHModelContainer _context;
        public WindowLgoty(JKHModelContainer context)
        {
            DataContext = this;
            _context = context;
            var lgoty = _context.ЛьготаSet.ToList();
            _lgoty = lgoty.Select(r => new LgotyListItem(false, r)).ToList();

            InitializeComponent();
        }

        public List<Льгота> GetSelectedLgoty()
        {
            return _lgoty.Where(s => s._selected).Select(s=>s._lgota).ToList();
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
