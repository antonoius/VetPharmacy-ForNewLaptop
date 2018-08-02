namespace VetPharmacy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InvoiceItem",
                c => new
                    {
                        InvoiceItemId = c.Int(nullable: false, identity: true),
                        Item_shipment_id = c.Int(nullable: false),
                        Quantity = c.Double(nullable: false),
                        Invoice_id = c.Int(nullable: false),
                        ITemCost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.InvoiceItemId)
                .ForeignKey("dbo.Invoice", t => t.Invoice_id)
                .ForeignKey("dbo.Shipment", t => t.Item_shipment_id)
                .Index(t => t.Item_shipment_id)
                .Index(t => t.Invoice_id);
            
            CreateTable(
                "dbo.Invoice",
                c => new
                    {
                        InvoiceId = c.Int(nullable: false, identity: true),
                        InvoiceDate = c.DateTime(nullable: false),
                        InvoiceTotalMoney = c.Double(nullable: false),
                        InvoiceDiscount = c.Int(nullable: false),
                        InvoiceType_id = c.Int(nullable: false),
                        InvoiceItemNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InvoiceId)
                .ForeignKey("dbo.InvoiceType", t => t.InvoiceType_id)
                .Index(t => t.InvoiceType_id);
            
            CreateTable(
                "dbo.InvoiceType",
                c => new
                    {
                        InvoiceTypeId = c.Int(nullable: false, identity: true),
                        InvoiceTypeName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.InvoiceTypeId);
            
            CreateTable(
                "dbo.Shipment",
                c => new
                    {
                        ShipmentId = c.Int(nullable: false, identity: true),
                        ShipmentAmount = c.Int(nullable: false),
                        OriginalPrice = c.Double(nullable: false),
                        TradPrice = c.Double(nullable: false),
                        Order_id = c.Int(nullable: false),
                        ShipmentRemainderAmount = c.Double(nullable: false),
                        ShipmentMedicine_id = c.Int(),
                    })
                .PrimaryKey(t => t.ShipmentId)
                .ForeignKey("dbo.Medicine", t => t.ShipmentMedicine_id)
                .ForeignKey("dbo.Order", t => t.Order_id)
                .Index(t => t.Order_id)
                .Index(t => t.ShipmentMedicine_id);
            
            CreateTable(
                "dbo.Medicine",
                c => new
                    {
                        MedicineId = c.Int(nullable: false, identity: true),
                        MedicineName = c.String(nullable: false, maxLength: 50),
                        Unit_Id = c.Int(nullable: false),
                        MedicineCapacity = c.Int(nullable: false),
                        Comment = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.MedicineId)
                .ForeignKey("dbo.Unit", t => t.Unit_Id)
                .Index(t => t.Unit_Id);
            
            CreateTable(
                "dbo.Unit",
                c => new
                    {
                        UnitId = c.Int(nullable: false, identity: true),
                        UnitName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.UnitId);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        OrderTotalMoney = c.Double(nullable: false),
                        Supplier_SupplierId = c.Int(),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Suppliers", t => t.Supplier_SupplierId)
                .Index(t => t.Supplier_SupplierId);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        SupplierId = c.Int(nullable: false, identity: true),
                        SupplierName = c.String(),
                    })
                .PrimaryKey(t => t.SupplierId);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order", "Supplier_SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.Shipment", "Order_id", "dbo.Order");
            DropForeignKey("dbo.Medicine", "Unit_Id", "dbo.Unit");
            DropForeignKey("dbo.Shipment", "ShipmentMedicine_id", "dbo.Medicine");
            DropForeignKey("dbo.InvoiceItem", "Item_shipment_id", "dbo.Shipment");
            DropForeignKey("dbo.Invoice", "InvoiceType_id", "dbo.InvoiceType");
            DropForeignKey("dbo.InvoiceItem", "Invoice_id", "dbo.Invoice");
            DropIndex("dbo.Order", new[] { "Supplier_SupplierId" });
            DropIndex("dbo.Medicine", new[] { "Unit_Id" });
            DropIndex("dbo.Shipment", new[] { "ShipmentMedicine_id" });
            DropIndex("dbo.Shipment", new[] { "Order_id" });
            DropIndex("dbo.Invoice", new[] { "InvoiceType_id" });
            DropIndex("dbo.InvoiceItem", new[] { "Invoice_id" });
            DropIndex("dbo.InvoiceItem", new[] { "Item_shipment_id" });
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.Suppliers");
            DropTable("dbo.Order");
            DropTable("dbo.Unit");
            DropTable("dbo.Medicine");
            DropTable("dbo.Shipment");
            DropTable("dbo.InvoiceType");
            DropTable("dbo.Invoice");
            DropTable("dbo.InvoiceItem");
        }
    }
}
