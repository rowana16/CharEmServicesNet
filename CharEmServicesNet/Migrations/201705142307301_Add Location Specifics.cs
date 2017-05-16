namespace CharEmServicesNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLocationSpecifics : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountyId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Counties", t => t.CountyId, cascadeDelete: true)
                .Index(t => t.CountyId);
            
            CreateTable(
                "dbo.Counties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Locations", "CityId", c => c.Int());
            AddColumn("dbo.Locations", "CountyId", c => c.Int(nullable: false));
            CreateIndex("dbo.Locations", "CityId");
            CreateIndex("dbo.Locations", "CountyId");
            AddForeignKey("dbo.Locations", "CityId", "dbo.Cities", "Id");
            AddForeignKey("dbo.Locations", "CountyId", "dbo.Counties", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Locations", "CountyId", "dbo.Counties");
            DropForeignKey("dbo.Locations", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Cities", "CountyId", "dbo.Counties");
            DropIndex("dbo.Cities", new[] { "CountyId" });
            DropIndex("dbo.Locations", new[] { "CountyId" });
            DropIndex("dbo.Locations", new[] { "CityId" });
            DropColumn("dbo.Locations", "CountyId");
            DropColumn("dbo.Locations", "CityId");
            DropTable("dbo.Counties");
            DropTable("dbo.Cities");
        }
    }
}
