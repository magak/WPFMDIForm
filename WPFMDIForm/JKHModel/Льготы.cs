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
    
    public partial class Льготы
    {
        public int ID { get; set; }
        public string Льгота { get; set; }
        public int Значение_льгота { get; set; }
        public byte[] Общая { get; set; }
    
        public virtual Жильцы Жильцы { get; set; }
    }
}
