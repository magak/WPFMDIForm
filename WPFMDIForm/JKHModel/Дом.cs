//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WPFMDIForm.JKHModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Дом
    {
        public Дом()
        {
            this.Квартира = new HashSet<Квартира>();
        }
    
        public int ID { get; set; }
        public string Адрес { get; set; }
        public decimal Площадь { get; set; }
    
        public virtual ICollection<Квартира> Квартира { get; set; }
    }
}
