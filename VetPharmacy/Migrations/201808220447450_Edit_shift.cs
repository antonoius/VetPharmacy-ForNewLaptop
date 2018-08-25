namespace VetPharmacy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit_shift : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Shifts", "EndDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Shifts", "EndDate", c => c.DateTime(nullable: false));
        }
    }
}
