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
    
    public partial class EmlakTipi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmlakTipi()
        {
            this.IlanDetay = new HashSet<IlanDetay>();
        }
    
        public int EmlakTipiID { get; set; }
        public string TipAdi { get; set; }
        public bool Durum { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IlanDetay> IlanDetay { get; set; }
    }
}