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
    
    public partial class Квартира
    {
        public Квартира()
        {
            this.Счетчик = new HashSet<Счетчик>();
            this.Жилец = new HashSet<Жилец>();
        }
    
        public int Id { get; set; }
        public decimal Площадь_квартиры { get; set; }
        public short Номер_квартиры { get; set; }
    
        public virtual ICollection<Счетчик> Счетчик { get; set; }
        public virtual ICollection<Жилец> Жилец { get; set; }
        public virtual Дом Дом { get; set; }
    }
}
