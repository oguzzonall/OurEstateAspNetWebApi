﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class eemlakDBEntities : DbContext
    {
        public eemlakDBEntities()
            : base("name=eemlakDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Adres> Adres { get; set; }
        public virtual DbSet<Begenmeler> Begenmeler { get; set; }
        public virtual DbSet<EmlakTipi> EmlakTipi { get; set; }
        public virtual DbSet<Goruntulenmeler> Goruntulenmeler { get; set; }
        public virtual DbSet<IlanDetay> IlanDetay { get; set; }
        public virtual DbSet<Ilanlar> Ilanlar { get; set; }
        public virtual DbSet<Ilceler> Ilceler { get; set; }
        public virtual DbSet<Kisiler> Kisiler { get; set; }
        public virtual DbSet<KisiRol> KisiRol { get; set; }
        public virtual DbSet<Resimler> Resimler { get; set; }
        public virtual DbSet<Roller> Roller { get; set; }
        public virtual DbSet<Sehirler> Sehirler { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Yorumlar> Yorumlar { get; set; }
    }
}
