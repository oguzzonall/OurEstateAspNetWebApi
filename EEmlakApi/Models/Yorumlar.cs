//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EEmlakApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Yorumlar
    {
        public int YorumID { get; set; }
        public string Icerik { get; set; }
        public int KisiId { get; set; }
        public int IlanId { get; set; }
        public System.DateTime YorumTarihi { get; set; }
        public string YorumBasligi { get; set; }
    
        public virtual Kisiler Kisiler { get; set; }
    }
}