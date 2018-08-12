namespace VetPharmacy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addunitcapacityentity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Unit", "UnitCapacity", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Unit", "UnitCapacity");
        }
    }
}
