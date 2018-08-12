namespace VetPharmacy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change_Unit_strcuture : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.SaleMethods", new[] { "Medicine_MedicineId" });
            AddColumn("dbo.SaleMethods", "Medicine_id", c => c.Int(nullable: false));
            CreateIndex("dbo.SaleMethods", "medicine_MedicineId");
            DropColumn("dbo.SaleMethods", "Shipment_id");
            DropColumn("dbo.SaleMethods", "TradPrice");
            DropColumn("dbo.SaleMethods", "OriginalPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SaleMethods", "OriginalPrice", c => c.Double(nullable: false));
            AddColumn("dbo.SaleMethods", "TradPrice", c => c.Double(nullable: false));
            AddColumn("dbo.SaleMethods", "Shipment_id", c => c.Int(nullable: false));
            DropIndex("dbo.SaleMethods", new[] { "medicine_MedicineId" });
            DropColumn("dbo.SaleMethods", "Medicine_id");
            CreateIndex("dbo.SaleMethods", "Medicine_MedicineId");
        }
    }
}
