using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFMDIForm.JKHModel
{
    public partial class Счетчик
    {
        public string Description
        {
            get
            {                
                return string.Format("{0} {1} {2}", this.Код_счетчика, this.Квартира.Номер_квартиры, 
                    this.Квартира.Дом.Адрес);
            }
        }
    }
}
