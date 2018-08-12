namespace VetPharmacy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClumonToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "ShipmentNumber", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "ShipmentNumber");
        }
    }
}
