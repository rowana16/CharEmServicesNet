namespace CharEmServicesNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserRoles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServiceProviders", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.ServiceRecipients", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AddColumn("dbo.AspNetUsers", "DisplayName", c => c.String());
            AddColumn("dbo.AspNetUsers", "Phone", c => c.String());
            CreateIndex("dbo.ServiceProviders", "ApplicationUser_Id");
            CreateIndex("dbo.ServiceRecipients", "ApplicationUser_Id");
            AddForeignKey("dbo.ServiceProviders", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.ServiceRecipients", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServiceRecipients", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ServiceProviders", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ServiceRecipients", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ServiceProviders", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.AspNetUsers", "Phone");
            DropColumn("dbo.AspNetUsers", "DisplayName");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
            DropColumn("dbo.ServiceRecipients", "ApplicationUser_Id");
            DropColumn("dbo.ServiceProviders", "ApplicationUser_Id");
        }
    }
}
