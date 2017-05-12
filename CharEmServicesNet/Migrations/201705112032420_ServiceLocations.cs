namespace CharEmServicesNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ServiceLocations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ServiceLocations",
                c => new
                    {
                        Service_Id = c.Int(nullable: false),
                        Location_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Service_Id, t.Location_Id })
                .ForeignKey("dbo.Services", t => t.Service_Id, cascadeDelete: true)
                .ForeignKey("dbo.Locations", t => t.Location_Id, cascadeDelete: true)
                .Index(t => t.Service_Id)
                .Index(t => t.Location_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServiceLocations", "Location_Id", "dbo.Locations");
            DropForeignKey("dbo.ServiceLocations", "Service_Id", "dbo.Services");
            DropIndex("dbo.ServiceLocations", new[] { "Location_Id" });
            DropIndex("dbo.ServiceLocations", new[] { "Service_Id" });
            DropTable("dbo.ServiceLocations");
        }
    }
}
