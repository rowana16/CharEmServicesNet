namespace CharEmServicesNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProviderDescriptions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServiceProviders", "Description", c => c.String());
            AddColumn("dbo.ServiceRecipients", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServiceRecipients", "Description");
            DropColumn("dbo.ServiceProviders", "Description");
        }
    }
}
