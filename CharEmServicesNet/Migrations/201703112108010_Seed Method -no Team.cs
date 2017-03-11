namespace CharEmServicesNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedMethodnoTeam : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.ServiceRecipients", "TeamId", "dbo.Teams");
            DropIndex("dbo.ServiceRecipients", new[] { "TeamId" });
            DropIndex("dbo.AspNetUsers", new[] { "Team_Id" });
            DropColumn("dbo.ServiceRecipients", "TeamId");
            DropColumn("dbo.AspNetUsers", "Team_Id");
            DropTable("dbo.Teams");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "Team_Id", c => c.Int());
            AddColumn("dbo.ServiceRecipients", "TeamId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "Team_Id");
            CreateIndex("dbo.ServiceRecipients", "TeamId");
            AddForeignKey("dbo.ServiceRecipients", "TeamId", "dbo.Teams", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUsers", "Team_Id", "dbo.Teams", "Id");
        }
    }
}
