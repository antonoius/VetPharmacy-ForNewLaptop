namespace VetPharmacy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSupplierTable2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        SupplierId = c.Int(nullable: false, identity: true),
                        SupplierName = c.String(),
                    })
                .PrimaryKey(t => t.SupplierId);
            
            AddColumn("dbo.Order", "Supplier_SupplierId", c => c.Int());
            CreateIndex("dbo.Order", "Supplier_SupplierId");
            AddForeignKey("dbo.Order", "Supplier_SupplierId", "dbo.Suppliers", "SupplierId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order", "Supplier_SupplierId", "dbo.Suppliers");
            DropIndex("dbo.Order", new[] { "Supplier_SupplierId" });
            DropColumn("dbo.Order", "Supplier_SupplierId");
            DropTable("dbo.Suppliers");
        }
    }
}
