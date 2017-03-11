namespace CharEmServicesNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedMethod : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ServiceProviders", "TeamId", "dbo.Teams");
            DropIndex("dbo.ServiceProviders", new[] { "TeamId" });
            DropColumn("dbo.ServiceProviders", "TeamId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ServiceProviders", "TeamId", c => c.Int(nullable: false));
            CreateIndex("dbo.ServiceProviders", "TeamId");
            AddForeignKey("dbo.ServiceProviders", "TeamId", "dbo.Teams", "Id", cascadeDelete: true);
        }
    }
}
