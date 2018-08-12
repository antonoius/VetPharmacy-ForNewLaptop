namespace VetPharmacy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit_in_shipmnet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shipment", "WholesalePrice", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shipment", "WholesalePrice");
        }
    }
}
