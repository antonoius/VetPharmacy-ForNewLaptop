namespace VetPharmacy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_entity_to_medicine_table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Medicine", "MedicineCapacity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Medicine", "MedicineCapacity");
        }
    }
}
