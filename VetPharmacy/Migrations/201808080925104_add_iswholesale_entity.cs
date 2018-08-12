namespace VetPharmacy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_iswholesale_entity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Medicine", "IsWholeSale", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Medicine", "IsWholeSale");
        }
    }
}
