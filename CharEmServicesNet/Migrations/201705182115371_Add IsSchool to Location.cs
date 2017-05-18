namespace CharEmServicesNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsSchooltoLocation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Locations", "IsSchool", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Locations", "IsSchool");
        }
    }
}
