namespace VetPharmacy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aff_user_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserMes",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        UserPassword = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Shifts",
                c => new
                    {
                        ShiftId = c.Int(nullable: false, identity: true),
                        User_id = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: true),
                        TotalMoney = c.Double(nullable: true),
                        user_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.ShiftId)
                .ForeignKey("dbo.UserMes", t => t.user_UserId)
                .Index(t => t.user_UserId);
            
            AddColumn("dbo.Invoice", "Shift_ShiftId", c => c.Int());
            CreateIndex("dbo.Invoice", "Shift_ShiftId");
            AddForeignKey("dbo.Invoice", "Shift_ShiftId", "dbo.Shifts", "ShiftId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Shifts", "user_UserId", "dbo.UserMes");
            DropForeignKey("dbo.Invoice", "Shift_ShiftId", "dbo.Shifts");
            DropIndex("dbo.Shifts", new[] { "user_UserId" });
            DropIndex("dbo.Invoice", new[] { "Shift_ShiftId" });
            DropColumn("dbo.Invoice", "Shift_ShiftId");
            DropTable("dbo.Shifts");
            DropTable("dbo.UserMes");
        }
    }
}
