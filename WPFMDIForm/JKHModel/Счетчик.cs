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
    
    public partial class Счетчик
    {
        public int Код_счетчика { get; set; }
    
        public virtual Квартира Квартира { get; set; }
        public virtual Услуга Услуга { get; set; }
        public virtual Показания_квартир Показания_квартир { get; set; }
    }
}
