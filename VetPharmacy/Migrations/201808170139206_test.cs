namespace VetPharmacy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Order", "ShipmentNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Order", "ShipmentNumber", c => c.Int(nullable: false));
            
        }
    }
}
