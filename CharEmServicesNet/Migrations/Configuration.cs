namespace CharEmServicesNet.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CharEmServicesNet.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CharEmServicesNet.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if(!context.Roles.Any(r=>r.Name == "UnitedWayAdmin"))
            {
                roleManager.Create(new IdentityRole { Name = "UnitedWayAdmin" });
            }
            if (!context.Roles.Any(r => r.Name == "ServiceProvider"))
            {
                roleManager.Create(new IdentityRole { Name = "ServiceProvider" });
            }
            if (!context.Roles.Any(r => r.Name == "ServiceRecipient"))
            {
                roleManager.Create(new IdentityRole { Name = "ServiceRecipient" });
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.ServiceTypes.AddOrUpdate(x => x.Id, new ServiceType() { Id = 1, Name = "After School", Description = "Provided After 3PM" });
            context.Addresses.AddOrUpdate(x => x.Id, new Address() { Id = 1, Address1 = "12 3rd St", Address2 = "Apt D", City = "Efigh", State = "JK", Zip = "12345" }  );
            context.Locations.AddOrUpdate(x => x.Id, new Location() { Id = 1, LocationName = "Main City", LocationDescription = "Home Of CharEm" } );
            context.OrganizationTypes.AddOrUpdate(x => x.Id, new OrganizationType() { Id = 1, TypeName = "Non-Profit", TypeDescription = "501.3c" });
            context.Services.AddOrUpdate(x => x.Id, new Service() { Id = 1, ServiceName = "Mentoring", ServiceDetails = "Adult Help For Children", ServiceTypeId = 1 });
            context.ServiceProviders.AddOrUpdate(x => x.Id, new ServiceProvider() { Id = 1, OrganizationName = "CharEmTest", AddressId = 1, Description = "Main Test", OrganizationTypeId = 1, UserId = "635f6aef-3d64-468c-8ffe-8da19d0702cc" });
            context.ServiceRecipients.AddOrUpdate(x => x.Id, new ServiceRecipient() { Id = 1, OrganizationName = "CharEmTest 2", AddressId = 1, Description = "Main Test 2", OrganizationTypeId = 1, UserId = "635f6aef-3d64-468c-8ffe-8da19d0702cc" });
           
        }
    }
}
