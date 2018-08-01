namespace VetPharmacy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove_entity_from_unit_table : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Unit", "UnitCapacity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Unit", "UnitCapacity", c => c.Int(nullable: false));
        }
    }
}
