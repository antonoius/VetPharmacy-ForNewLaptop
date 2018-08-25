namespace VetPharmacy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class invoice_editte : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Invoice", new[] { "Shift_ShiftId" });
            AddColumn("dbo.Invoice", "Shift_id", c => c.Int(nullable: false));
            CreateIndex("dbo.Invoice", "shift_ShiftId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Invoice", new[] { "shift_ShiftId" });
            DropColumn("dbo.Invoice", "Shift_id");
            CreateIndex("dbo.Invoice", "Shift_ShiftId");
        }
    }
}
