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
    
    public partial class Показания_ОДУ
    {
        public int Id { get; set; }
        public string Показание_ГВС { get; set; }
        public string Показание_ХВС { get; set; }
    
        public virtual Календарь Календарь { get; set; }
        public virtual Дом Дом { get; set; }
    }
}
