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
    
    public partial class Показания_квартир
    {
        public int Id { get; set; }
        public short Значение_показания_кв { get; set; }
    
        public virtual Календарь Календарь { get; set; }
        public virtual Счетчик Счетчик { get; set; }
    }
}
