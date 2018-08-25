namespace VetPharmacy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_entity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Unit", "comment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Unit", "comment");
        }
    }
}
