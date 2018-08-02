namespace VetPharmacy
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Models;

    public partial class VetPharmaDB : DbContext
    {
        public VetPharmaDB()
            : base("name=VetPharmaDB")
        {
        }

        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceItem> InvoiceItems { get; set; }
        public virtual DbSet<InvoiceType> InvoiceTypes { get; set; }
        public virtual DbSet<Medicine> Medicines { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Shipment> Shipments { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<Supplier> Suppliers { set; get; }
        public virtual DbSet<SaleMethod> SaleMethods { set; get; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice>()
                .HasMany(e => e.InvoiceItems)
                .WithRequired(e => e.Invoice)
                .HasForeignKey(e => e.Invoice_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<InvoiceType>()
                .HasMany(e => e.Invoices)
                .WithRequired(e => e.InvoiceType)
                .HasForeignKey(e => e.InvoiceType_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Medicine>()
                .HasMany(e => e.Shipments)
                .WithOptional(e => e.Medicine)
                .HasForeignKey(e => e.ShipmentMedicine_id);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.Shipments)
                .WithRequired(e => e.Order)
                .HasForeignKey(e => e.Order_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Shipment>()
                .HasMany(e => e.InvoiceItems)
                .WithRequired(e => e.Shipment)
                .HasForeignKey(e => e.Item_shipment_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Unit>()
                .HasMany(e => e.Medicines)
                .WithRequired(e => e.Unit)
                .HasForeignKey(e => e.Unit_Id)
                .WillCascadeOnDelete(false);
        }
    }
}
