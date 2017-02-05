using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFMDIForm.JKHModel
{
    public partial class Календарь
    {
        public string Description
        {
            get
            {
                return string.Format("{0} {1}", this.Месяц, this.Год);
            }
        }
    }
}
