namespace VetPharmacy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_entity_to_userme : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserMes", "UserEmail", c => c.String());
            AddColumn("dbo.Shifts", "InvoiceNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shifts", "InvoiceNumber");
            DropColumn("dbo.UserMes", "UserEmail");
        }
    }
}
