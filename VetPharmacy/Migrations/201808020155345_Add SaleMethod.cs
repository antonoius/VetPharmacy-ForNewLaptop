namespace VetPharmacy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSaleMethod : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SaleMethods",
                c => new
                    {
                        SaleMethodId = c.Int(nullable: false, identity: true),
                        Shipment_id = c.Int(nullable: false),
                        Unit_id = c.Int(nullable: false),
                        TradPrice = c.Double(nullable: false),
                        OriginalPrice = c.Double(nullable: false),
                        Capacity = c.Double(nullable: false),
                        Shipment_ShipmentId = c.Int(),
                    })
                .PrimaryKey(t => t.SaleMethodId)
                .ForeignKey("dbo.Shipment", t => t.Shipment_ShipmentId)
                .Index(t => t.Shipment_ShipmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SaleMethods", "Shipment_ShipmentId", "dbo.Shipment");
            DropIndex("dbo.SaleMethods", new[] { "Shipment_ShipmentId" });
            DropTable("dbo.SaleMethods");
        }
    }
}
