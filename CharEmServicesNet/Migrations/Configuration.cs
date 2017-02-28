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
        }
    }
}
