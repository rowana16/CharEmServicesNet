namespace CharEmServicesNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOtherTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zip = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServiceProviders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrganizationName = c.String(),
                        AddressId = c.Int(nullable: false),
                        OrganizationTypeId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        TeamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: true)
                .ForeignKey("dbo.OrganizationTypes", t => t.OrganizationTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.AddressId)
                .Index(t => t.OrganizationTypeId)
                .Index(t => t.UserId)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LocationName = c.String(),
                        LocationDescription = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServiceRecipients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrganizationName = c.String(),
                        AddressId = c.Int(nullable: false),
                        OrganizationTypeId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        TeamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: true)
                .ForeignKey("dbo.OrganizationTypes", t => t.OrganizationTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.AddressId)
                .Index(t => t.OrganizationTypeId)
                .Index(t => t.UserId)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.OrganizationTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeName = c.String(),
                        TypeDescription = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ServiceName = c.String(),
                        ServiceDetails = c.String(),
                        ServiceTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ServiceTypes", t => t.ServiceTypeId, cascadeDelete: true)
                .Index(t => t.ServiceTypeId);
            
            CreateTable(
                "dbo.ServiceTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LocationServiceProviders",
                c => new
                    {
                        Location_Id = c.Int(nullable: false),
                        ServiceProvider_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Location_Id, t.ServiceProvider_Id })
                .ForeignKey("dbo.Locations", t => t.Location_Id, cascadeDelete: true)
                .ForeignKey("dbo.ServiceProviders", t => t.ServiceProvider_Id, cascadeDelete: true)
                .Index(t => t.Location_Id)
                .Index(t => t.ServiceProvider_Id);
            
            CreateTable(
                "dbo.ServiceRecipientLocations",
                c => new
                    {
                        ServiceRecipient_Id = c.Int(nullable: false),
                        Location_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ServiceRecipient_Id, t.Location_Id })
                .ForeignKey("dbo.ServiceRecipients", t => t.ServiceRecipient_Id, cascadeDelete: true)
                .ForeignKey("dbo.Locations", t => t.Location_Id, cascadeDelete: true)
                .Index(t => t.ServiceRecipient_Id)
                .Index(t => t.Location_Id);
            
            CreateTable(
                "dbo.ServiceServiceProviders",
                c => new
                    {
                        Service_Id = c.Int(nullable: false),
                        ServiceProvider_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Service_Id, t.ServiceProvider_Id })
                .ForeignKey("dbo.Services", t => t.Service_Id, cascadeDelete: true)
                .ForeignKey("dbo.ServiceProviders", t => t.ServiceProvider_Id, cascadeDelete: true)
                .Index(t => t.Service_Id)
                .Index(t => t.ServiceProvider_Id);
            
            CreateTable(
                "dbo.ServiceServiceRecipients",
                c => new
                    {
                        Service_Id = c.Int(nullable: false),
                        ServiceRecipient_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Service_Id, t.ServiceRecipient_Id })
                .ForeignKey("dbo.Services", t => t.Service_Id, cascadeDelete: true)
                .ForeignKey("dbo.ServiceRecipients", t => t.ServiceRecipient_Id, cascadeDelete: true)
                .Index(t => t.Service_Id)
                .Index(t => t.ServiceRecipient_Id);
            
            AddColumn("dbo.AspNetUsers", "Team_Id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "ServiceRecipient_Id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "ServiceProvider_Id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Address_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Team_Id");
            CreateIndex("dbo.AspNetUsers", "ServiceRecipient_Id");
            CreateIndex("dbo.AspNetUsers", "ServiceProvider_Id");
            CreateIndex("dbo.AspNetUsers", "Address_Id");
            AddForeignKey("dbo.AspNetUsers", "Team_Id", "dbo.Teams", "Id");
            AddForeignKey("dbo.AspNetUsers", "ServiceRecipient_Id", "dbo.ServiceRecipients", "Id");
            AddForeignKey("dbo.AspNetUsers", "ServiceProvider_Id", "dbo.ServiceProviders", "Id");
            AddForeignKey("dbo.AspNetUsers", "Address_Id", "dbo.Addresses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Address_Id", "dbo.Addresses");
            DropForeignKey("dbo.AspNetUsers", "ServiceProvider_Id", "dbo.ServiceProviders");
            DropForeignKey("dbo.ServiceProviders", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ServiceProviders", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.AspNetUsers", "ServiceRecipient_Id", "dbo.ServiceRecipients");
            DropForeignKey("dbo.ServiceRecipients", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ServiceRecipients", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.AspNetUsers", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.Services", "ServiceTypeId", "dbo.ServiceTypes");
            DropForeignKey("dbo.ServiceServiceRecipients", "ServiceRecipient_Id", "dbo.ServiceRecipients");
            DropForeignKey("dbo.ServiceServiceRecipients", "Service_Id", "dbo.Services");
            DropForeignKey("dbo.ServiceServiceProviders", "ServiceProvider_Id", "dbo.ServiceProviders");
            DropForeignKey("dbo.ServiceServiceProviders", "Service_Id", "dbo.Services");
            DropForeignKey("dbo.ServiceRecipients", "OrganizationTypeId", "dbo.OrganizationTypes");
            DropForeignKey("dbo.ServiceProviders", "OrganizationTypeId", "dbo.OrganizationTypes");
            DropForeignKey("dbo.ServiceRecipientLocations", "Location_Id", "dbo.Locations");
            DropForeignKey("dbo.ServiceRecipientLocations", "ServiceRecipient_Id", "dbo.ServiceRecipients");
            DropForeignKey("dbo.ServiceRecipients", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.LocationServiceProviders", "ServiceProvider_Id", "dbo.ServiceProviders");
            DropForeignKey("dbo.LocationServiceProviders", "Location_Id", "dbo.Locations");
            DropForeignKey("dbo.ServiceProviders", "AddressId", "dbo.Addresses");
            DropIndex("dbo.ServiceServiceRecipients", new[] { "ServiceRecipient_Id" });
            DropIndex("dbo.ServiceServiceRecipients", new[] { "Service_Id" });
            DropIndex("dbo.ServiceServiceProviders", new[] { "ServiceProvider_Id" });
            DropIndex("dbo.ServiceServiceProviders", new[] { "Service_Id" });
            DropIndex("dbo.ServiceRecipientLocations", new[] { "Location_Id" });
            DropIndex("dbo.ServiceRecipientLocations", new[] { "ServiceRecipient_Id" });
            DropIndex("dbo.LocationServiceProviders", new[] { "ServiceProvider_Id" });
            DropIndex("dbo.LocationServiceProviders", new[] { "Location_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Address_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "ServiceProvider_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "ServiceRecipient_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Team_Id" });
            DropIndex("dbo.Services", new[] { "ServiceTypeId" });
            DropIndex("dbo.ServiceRecipients", new[] { "TeamId" });
            DropIndex("dbo.ServiceRecipients", new[] { "UserId" });
            DropIndex("dbo.ServiceRecipients", new[] { "OrganizationTypeId" });
            DropIndex("dbo.ServiceRecipients", new[] { "AddressId" });
            DropIndex("dbo.ServiceProviders", new[] { "TeamId" });
            DropIndex("dbo.ServiceProviders", new[] { "UserId" });
            DropIndex("dbo.ServiceProviders", new[] { "OrganizationTypeId" });
            DropIndex("dbo.ServiceProviders", new[] { "AddressId" });
            DropColumn("dbo.AspNetUsers", "Address_Id");
            DropColumn("dbo.AspNetUsers", "ServiceProvider_Id");
            DropColumn("dbo.AspNetUsers", "ServiceRecipient_Id");
            DropColumn("dbo.AspNetUsers", "Team_Id");
            DropTable("dbo.ServiceServiceRecipients");
            DropTable("dbo.ServiceServiceProviders");
            DropTable("dbo.ServiceRecipientLocations");
            DropTable("dbo.LocationServiceProviders");
            DropTable("dbo.Teams");
            DropTable("dbo.ServiceTypes");
            DropTable("dbo.Services");
            DropTable("dbo.OrganizationTypes");
            DropTable("dbo.ServiceRecipients");
            DropTable("dbo.Locations");
            DropTable("dbo.ServiceProviders");
            DropTable("dbo.Addresses");
        }
    }
}
