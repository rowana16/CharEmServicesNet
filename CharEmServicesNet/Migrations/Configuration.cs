namespace CharEmServicesNet.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Collections.Generic;
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

            if (!context.Roles.Any(r => r.Name == "UnitedWayAdmin"))
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
           
            context.Addresses.AddOrUpdate(x => x.Address1,
                new Address { Address1 = "2350 Mitchell Park Drive", City = "Petoskey", State = "MI", Zip = "49770" },
                new Address { Address1 = "423 Porter Street", City = "Petoskey", State = "MI", Zip = "49770" },
                new Address { Address1 = "8791 McBride Park Court", City = "Harbor Springs", State = "MI", Zip = "49737" },
                new Address { Address1 = "415 State St.", City = "Petoskey", State = "MI", Zip = "49770" },
                new Address { Address1 = "220 West Garfield", City = "Charlevoix", State = "MI", Zip = "49711" },
                new Address { Address1 = "03001 Church Road", City = "Petoskey", State = "MI", Zip = "49770" },
                new Address { Address1 = "434 East Lake Street", City = "Petoskey", State = "MI", Zip = "49770" }

            );

            context.Counties.AddOrUpdate(x => x.Name,
                new County { Name = "Emmet" },
                new County { Name = "Charlevoix" },
                new County { Name = "Antrim" },
                new County { Name = "Cheboygan" },
                new County { Name = "Otsego" },
                new County { Name = "None" }

            );

            context.SaveChanges();

            var County = context.Counties.ToDictionary(p => p.Name, p => p.Id);

            context.Cities.AddOrUpdate(x => x.Name,
                new City { Name = "Boyne City", CountyId = County["Charlevoix"] },
                new City { Name = "Boyne Falls", CountyId = County["Charlevoix"] },
                new City { Name = "Central Lake", CountyId = County["Antrim"] },
                new City { Name = "Ellsworth  ", CountyId = County["Antrim"] },
                new City { Name = "Harbor Springs", CountyId = County["Emmet"] },
                new City { Name = "Pellston", CountyId = County["Emmet"] },
                new City { Name = "East Jordan", CountyId = County["Charlevoix"] },
                new City { Name = "Petoskey", CountyId = County["Emmet"] },
                new City { Name = "Cheboygan", CountyId = County["Cheboygan"] },
                new City { Name = "Alanson", CountyId = County["Emmet"] },
                new City { Name = "Wolverine", CountyId = County["Cheboygan"] },
                new City { Name = "Vanderbilt", CountyId = County["Otsego"] },
                new City { Name = "Gaylord", CountyId = County["Otsego"] },
                new City { Name = "Bellaire", CountyId = County["Antrim"] },
                new City { Name = "Alba", CountyId = County["Antrim"] },
                new City { Name = "Mancelona", CountyId = County["Antrim"] },
                new City { Name = "Mackinaw", CountyId = County["Cheboygan"] },
                new City { Name = "None", CountyId = County["None"] }

            );

            context.SaveChanges();

            var City = context.Cities.ToDictionary(p => p.Name, p => p.Id);
            var Address = context.Addresses.ToDictionary(p => p.Address1, p => p.Id);

            context.OrganizationTypes.AddOrUpdate(x => x.TypeName,
                new OrganizationType { TypeName = "Local Service" },
                new OrganizationType {  TypeName = "State Service" },
                new OrganizationType {  TypeName = "Federal Service" },
                new OrganizationType {  TypeName = "Faith Based Service"},
                new OrganizationType {  TypeName = "General Service"}
            );

            context.SaveChanges();

            context.ServiceProviders.AddOrUpdate(x => x.OrganizationName,
               new ServiceProvider { OrganizationName = "Big Brothers / Big Sisters", Description = "We Provide Children With Strong And Enduring, Professionally Supported 1-To-1 Relationships That Change Their Lives For The Better. Our Goal Is To Help Children Obtain Higher Aspirations, Greater Confidence, And Better Relationships; Avoid Risky Behaviors And Obtain Educational Success.  We Offer Mentoring Opportunities Throughout The Community, As Well As School-Based Mentoring Programs In Specific Elementary Schools.", AddressId = Address["2350 Mitchell Park Drive"], OrganizationTypeId = 5 },
               new ServiceProvider { OrganizationName = "Women's Resource Center of N. Michigan", Description = "We Provide Violence Prevention Programs, Education And Employment Services, Education Support For 'Safe Bodies', Counseling Services For Domestic Violence And Sexual Assault, And Child Abuse / Child Sexual Assault Counseling. ", AddressId = Address["423 Porter Street"], OrganizationTypeId = 5 },
               new ServiceProvider { OrganizationName = "Manna Food Project", Description = "We Help To Feed The Hungry In Northern Michigan. We Operate A Food Bank Providing Low Cost Food To Partner Food Pantries And Community Kitchens, A Food Rescue Program Which Collects Perishable Food For Distribution To The Hungry, A Weekly Pantry Which Provides Food To Families, And The `Food 4 Kids` Program Which Distributes Backpacks Filled With Nutritional Food Items To Elementary Schools And Preschools To Help Carry At-Risk Students Through Each Weekend Of The School Year.", AddressId = Address["8791 McBride Park Court"], OrganizationTypeId = 5},
               new ServiceProvider { OrganizationName = "Northern Community Mediation", Description = "We Provide Programs Specific To Youth Including: Child Protection Mediation, Victim-Offender Reconciliation, School Attendance Mediation, First-Time Offender Shoplifting, And Preventative Shoplifting", AddressId = Address["415 State St."], OrganizationTypeId = 5},
               new ServiceProvider { OrganizationName = "Health Dept. of NW Michigan", Description = "We Run School Based Health Centers, The Safe Youth Coalition, And A Home Visiting Program For Pregnant And Parenting Youth.. We Provide Programming To Prevent Substance Use Disorder, Improve Nutrition Education, And Provide Hearing And Vision Screening, Immunizations, Sexual Health Services, Children'S Healthcare Access Program, Health Insurance Assistance.  We Also Help To Develop Policy, Systems And Environment Changes With Schools Including: Safe Routes To Schools, Smarter Lunchroom, And School Wellness Policies. ", AddressId = Address["220 West Garfield"], OrganizationTypeId = 5},
               new ServiceProvider { OrganizationName = "Camp Daggett", Description = "We Provide Programs Specific To Youth Including: Traditional Summer Camp, Leadership Programs And The School Climate Program", AddressId = Address["03001 Church Road"], OrganizationTypeId = 5},
               new ServiceProvider { OrganizationName = "YMCA", Description = "We Provide Transformational Youth Programs That Develop Character, Build Self Confidence, Promote Healthy Lifestyle Choices. We Offer: An Afterschool Childcare Program For Students K-5, A Summer Day Camp For Students Ages 5-15, Physical Programs For Ages 3-4, Y Winners Basketball & Soccer, Y Karate, Y Archery, Jr. & First Lego League, Art Programs And More! The Y Makes Sure That Everyone, Regardless Of Age, Income Or Background, Has The Opportunity To Learn Grown And Thrive. ", AddressId = Address["434 East Lake Street"], OrganizationTypeId = 5}
            );
                
            context.Locations.AddOrUpdate(x => x.LocationName, 
                new Location { LocationName = "Resident - County - Emmet", CountyId = County["Antrim"], CityId = City["None"], IsSchool = false },
                new Location { LocationName = "Resident - County - Charlevoix", CountyId = County["Charlevoix"], CityId = City["None"], IsSchool = false },
                new Location { LocationName = "Resident - County - Antrim", CountyId = County["Cheboygan"], CityId = City["None"], IsSchool = false },
                new Location { LocationName = "Resident - County - Cheboygan", CountyId = County["Emmet"], CityId = City["None"], IsSchool = false },
                new Location { LocationName = "Resident - County - Otsego", CountyId = County["Otsego"], CityId = City["None"], IsSchool = false },
                new Location { LocationName = "Resident - City - Alanson", CountyId = County["Emmet"], CityId = City["Alanson"], IsSchool = false },
                new Location { LocationName = "Resident - City - Alba", CountyId = County["Antrim"], CityId = City["Alba"], IsSchool = false },
                new Location { LocationName = "Resident - City - Bellaire", CountyId = County["Antrim"], CityId = City["Bellaire"], IsSchool = false },
                new Location { LocationName = "Resident - City - Boyne City", CountyId = County["Charlevoix"], CityId = City["Boyne City"], IsSchool = false },
                new Location { LocationName = "Resident - City - Boyne Falls", CountyId = County["Charlevoix"], CityId = City["Boyne Falls"], IsSchool = false },
                new Location { LocationName = "Resident - City - Central Lake", CountyId = County["Antrim"], CityId = City["Central Lake"], IsSchool = false },
                new Location { LocationName = "Resident - City - Cheboygan", CountyId = County["Cheboygan"], CityId = City["Cheboygan"], IsSchool = false },
                new Location { LocationName = "Resident - City - East Jordan", CountyId = County["Charlevoix"], CityId = City["East Jordan"], IsSchool = false },
                new Location { LocationName = "Resident - City - Ellsworth  ", CountyId = County["Antrim"], CityId = City["Ellsworth  "], IsSchool = false },
                new Location { LocationName = "Resident - City - Gaylord", CountyId = County["Otsego"], CityId = City["Gaylord"], IsSchool = false },
                new Location { LocationName = "Resident - City - Harbor Springs", CountyId = County["Emmet"], CityId = City["Harbor Springs"], IsSchool = false },
                new Location { LocationName = "Resident - City - Mackinaw", CountyId = County["Cheboygan"], CityId = City["Mackinaw"], IsSchool = false },
                new Location { LocationName = "Resident - City - Mancelona", CountyId = County["Antrim"], CityId = City["Mancelona"], IsSchool = false },
                new Location { LocationName = "Resident - City - Pellston", CountyId = County["Emmet"], CityId = City["Pellston"], IsSchool = false },
                new Location { LocationName = "Resident - City - Petoskey", CountyId = County["Emmet"], CityId = City["Petoskey"], IsSchool = false },
                new Location { LocationName = "Resident - City - Vanderbilt", CountyId = County["Otsego"], CityId = City["Vanderbilt"], IsSchool = false },
                new Location { LocationName = "Resident - City - Wolverine", CountyId = County["Cheboygan"], CityId = City["Wolverine"], IsSchool = false },
                new Location { LocationName = "Alanson School", CountyId = County["Emmet"], CityId = City["Alanson"], IsSchool = true },
                new Location { LocationName = "Alba Public School", CountyId = County["Antrim"], CityId = City["Alba"], IsSchool = true },
                new Location { LocationName = "Bellaire Elementary", CountyId = County["Antrim"], CityId = City["Bellaire"], IsSchool = true },
                new Location { LocationName = "Bellaire Middle / High", CountyId = County["Antrim"], CityId = City["Bellaire"], IsSchool = true },
                new Location { LocationName = "Boyne City Elementary", CountyId = County["Charlevoix"], CityId = City["Boyne City"], IsSchool = true },
                new Location { LocationName = "Boyne City High", CountyId = County["Charlevoix"], CityId = City["Boyne City"], IsSchool = true },
                new Location { LocationName = "Boyne City Middle", CountyId = County["Charlevoix"], CityId = City["Boyne City"], IsSchool = true },
                new Location { LocationName = "Boyne Falls School", CountyId = County["Charlevoix"], CityId = City["Boyne Falls"], IsSchool = true },
                new Location { LocationName = "Central Lake Elementary", CountyId = County["Antrim"], CityId = City["Central Lake"], IsSchool = true },
                new Location { LocationName = "Central Lake Middle / High", CountyId = County["Antrim"], CityId = City["Central Lake"], IsSchool = true },
                new Location { LocationName = "Cheboygan - East Elementary", CountyId = County["Cheboygan"], CityId = City["Cheboygan"], IsSchool = true },
                new Location { LocationName = "Cheboygan - West Elementary", CountyId = County["Cheboygan"], CityId = City["Cheboygan"], IsSchool = true },
                new Location { LocationName = "Cheboygan High", CountyId = County["Cheboygan"], CityId = City["Cheboygan"], IsSchool = true },
                new Location { LocationName = "Cheboygan Middle", CountyId = County["Cheboygan"], CityId = City["Cheboygan"], IsSchool = true },
                new Location { LocationName = "East Jordan Elementary", CountyId = County["Charlevoix"], CityId = City["East Jordan"], IsSchool = true },
                new Location { LocationName = "East Jordan High", CountyId = County["Charlevoix"], CityId = City["East Jordan"], IsSchool = true },
                new Location { LocationName = "East Jordan Middle", CountyId = County["Charlevoix"], CityId = City["East Jordan"], IsSchool = true },
                new Location { LocationName = "Ellsworth - Hillcrest Elementary", CountyId = County["Antrim"], CityId = City["Ellsworth  "], IsSchool = true },
                new Location { LocationName = "Ellsworth - Prairie View Elementary", CountyId = County["Antrim"], CityId = City["Ellsworth  "], IsSchool = true },
                new Location { LocationName = "Ellsworth High", CountyId = County["Antrim"], CityId = City["Ellsworth  "], IsSchool = true },
                new Location { LocationName = "Ellsworth Middle", CountyId = County["Antrim"], CityId = City["Ellsworth  "], IsSchool = true },
                new Location { LocationName = "Gaylord - North Ohio Elementary", CountyId = County["Otsego"], CityId = City["Gaylord"], IsSchool = true },
                new Location { LocationName = "Gaylord - South Maple Elementary", CountyId = County["Otsego"], CityId = City["Gaylord"], IsSchool = true },
                new Location { LocationName = "Gaylord High", CountyId = County["Otsego"], CityId = City["Gaylord"], IsSchool = true },
                new Location { LocationName = "Gaylord Intermediate", CountyId = County["Otsego"], CityId = City["Gaylord"], IsSchool = true },
                new Location { LocationName = "Gaylord Middle", CountyId = County["Otsego"], CityId = City["Gaylord"], IsSchool = true },
                new Location { LocationName = "Harbor Springs - Black Bird Elementary", CountyId = County["Emmet"], CityId = City["Harbor Springs"], IsSchool = true },
                new Location { LocationName = "Harbor Springs - Shay Elementary", CountyId = County["Emmet"], CityId = City["Harbor Springs"], IsSchool = true },
                new Location { LocationName = "Harbor Springs High", CountyId = County["Emmet"], CityId = City["Harbor Springs"], IsSchool = true },
                new Location { LocationName = "Harbor Springs Middle", CountyId = County["Emmet"], CityId = City["Harbor Springs"], IsSchool = true },
                new Location { LocationName = "Mackinaw Elementary", CountyId = County["Cheboygan"], CityId = City["Mackinaw"], IsSchool = true },
                new Location { LocationName = "Mackinaw Middle / High", CountyId = County["Cheboygan"], CityId = City["Mackinaw"], IsSchool = true },
                new Location { LocationName = "Mancelona Elementary", CountyId = County["Antrim"], CityId = City["Mancelona"], IsSchool = true },
                new Location { LocationName = "Mancelona High", CountyId = County["Antrim"], CityId = City["Mancelona"], IsSchool = true },
                new Location { LocationName = "Mancelona Middle", CountyId = County["Antrim"], CityId = City["Mancelona"], IsSchool = true },
                new Location { LocationName = "Pellston Elementary", CountyId = County["Emmet"], CityId = City["Pellston"], IsSchool = true },
                new Location { LocationName = "Pellston Middle / High", CountyId = County["Emmet"], CityId = City["Pellston"], IsSchool = true },
                new Location { LocationName = "Petoskey - Central Elementary", CountyId = County["Emmet"], CityId = City["Petoskey"], IsSchool = true },
                new Location { LocationName = "Petoskey - Lincoln Elementary", CountyId = County["Emmet"], CityId = City["Petoskey"], IsSchool = true },
                new Location { LocationName = "Petoskey - Ottawa Elementary", CountyId = County["Emmet"], CityId = City["Petoskey"], IsSchool = true },
                new Location { LocationName = "Petoskey - Sheridan Elementary", CountyId = County["Emmet"], CityId = City["Petoskey"], IsSchool = true },
                new Location { LocationName = "Petoskey High", CountyId = County["Emmet"], CityId = City["Petoskey"], IsSchool = true },
                new Location { LocationName = "Petoskey Middle", CountyId = County["Emmet"], CityId = City["Petoskey"], IsSchool = true },
                new Location { LocationName = "Vanderbilt Area School", CountyId = County["Otsego"], CityId = City["Vanderbilt"], IsSchool = true },
                new Location { LocationName = "Wolverine Elementary", CountyId = County["Cheboygan"], CityId = City["Wolverine"], IsSchool = true },
                new Location { LocationName = "Wolverine High", CountyId = County["Cheboygan"], CityId = City["Wolverine"], IsSchool = true }

            );

            context.SaveChanges();

            context.ServiceTypes.AddOrUpdate(x => x.Name,
                new ServiceType { Name = "Behavioral Services", Description = " " },
                new ServiceType { Name = "Child Care", Description = " " },
                new ServiceType { Name = "Counseling", Description = " " },
                new ServiceType { Name = "Educational Services", Description = " " },
                new ServiceType { Name = "Financial Services", Description = " " },
                new ServiceType { Name = "Food Services", Description = " " },
                new ServiceType { Name = "Mentoring Services", Description = " " },
                new ServiceType { Name = "Recreation and Enrichment", Description = " " },
                new ServiceType { Name = "Substance Use and Health Services", Description = " " },
                new ServiceType { Name = "Volunteering", Description = " " }
            );

            //Location Variables
            context.SaveChanges();

            var YMCA = new List<string>() { "Petoskey  ", "Indian River" };
            var BigsCommunity = new List<string>() { "Emmet", "Charlevoix" };
            var BigsSchool = new List<string>() { "Harbor Springs - Black Bird Elementary", "Harbor Springs - Shay Elementary", "Petoskey - Lincoln Elementary", "Petoskey - Sheridan Elementary", "Boyne City Elementary", "Charlevoix Elementary" };
            var Mediation = new List<string>() { "Emmet", "Charlevoix" };
            var Womens = new List<string>() { "Emmet", "Antrim", "Cheboygan", "Otsego", "Charlevoix" };
            var MIHP = new List<string>() { "Bellaire", "Charlevoix", "Gaylord", "Mancelona", "Harbor Springs", "Petoskey" };
            var SexHealth = new List<string>() { "Charlevoix", "Gaylord", "Mancelona", "Harbor Springs", "Petoskey" };
            var Dental = new List<string>() { "Gaylord", "Mancelona", "Harbor Springs", "Petoskey", "Cheboygan", "East Jordan" };
            var HealthOther = new List<string>() { "Gaylord", "Mancelona", "Pellston", "Boyne City" };
            var serviceType = context.ServiceTypes.ToDictionary(s => s.Name, s => s.Id);
            

            context.Services.AddOrUpdate(x => x.ServiceName,
                new Service { ServiceName = "Jr. FIRST Lego League", ServiceProviders = context.ServiceProviders.Where(x => x.OrganizationName == "YMCA").ToList(), Locations = context.Locations.Where(x => YMCA.Contains(x.City.Name)).ToList(), ServiceTypeId = serviceType["Recreation and Enrichment"], ServiceRecipients = new List<ServiceRecipient>() },
                new Service { ServiceName = "FIRST Lego League", ServiceProviders = context.ServiceProviders.Where(x => x.OrganizationName == "YMCA").ToList(), Locations = context.Locations.Where(x => YMCA.Contains(x.City.Name)).ToList(), ServiceTypeId = serviceType["Recreation and Enrichment"], ServiceRecipients = new List<ServiceRecipient>() },
                new Service { ServiceName = "Art Programs", ServiceProviders = context.ServiceProviders.Where(x => x.OrganizationName == "YMCA").ToList(), Locations = context.Locations.Where(x => YMCA.Contains(x.City.Name)).ToList(), ServiceTypeId = serviceType["Recreation and Enrichment"], ServiceRecipients = new List<ServiceRecipient>() },
                new Service { ServiceName = "After School Child Care", ServiceProviders = context.ServiceProviders.Where(x => x.OrganizationName == "YMCA").ToList(), Locations = context.Locations.Where(x => YMCA.Contains(x.City.Name)).ToList(), ServiceTypeId = serviceType["Child Care"], ServiceRecipients = new List<ServiceRecipient>() },
                new Service { ServiceName = "Basketball", ServiceProviders = context.ServiceProviders.Where(x => x.OrganizationName == "YMCA").ToList(), Locations = context.Locations.Where(x => YMCA.Contains(x.City.Name)).ToList(), ServiceTypeId = serviceType["Recreation and Enrichment"], ServiceRecipients = new List<ServiceRecipient>() },
                new Service { ServiceName = "Soccer", ServiceProviders = context.ServiceProviders.Where(x => x.OrganizationName == "YMCA").ToList(), Locations = context.Locations.Where(x => YMCA.Contains(x.City.Name)).ToList(), ServiceTypeId = serviceType["Recreation and Enrichment"], ServiceRecipients = new List<ServiceRecipient>() },
                new Service { ServiceName = "Karate", ServiceProviders = context.ServiceProviders.Where(x => x.OrganizationName == "YMCA").ToList(), Locations = context.Locations.Where(x => YMCA.Contains(x.City.Name)).ToList(), ServiceTypeId = serviceType["Recreation and Enrichment"], ServiceRecipients = new List<ServiceRecipient>() },
                new Service { ServiceName = "Archery", ServiceProviders = context.ServiceProviders.Where(x => x.OrganizationName == "YMCA").ToList(), Locations = context.Locations.Where(x => YMCA.Contains(x.City.Name)).ToList(), ServiceTypeId = serviceType["Recreation and Enrichment"], ServiceRecipients = new List<ServiceRecipient>() },
                new Service { ServiceName = "Community-Based Mentoring (Big Brother / Big Sister)", ServiceProviders = context.ServiceProviders.Where(x => x.OrganizationName == "Big Brothers/Big Sisters").ToList(), Locations = context.Locations.Where(x => BigsCommunity.Contains(x.City.Name)).ToList(), ServiceTypeId = serviceType["Mentoring Services"], ServiceRecipients = new List<ServiceRecipient>() },
                new Service { ServiceName = "School-Based Mentoring (Big Brother / Big Sister)", ServiceProviders = context.ServiceProviders.Where(x => x.OrganizationName == "Big Brothers/Big Sisters").ToList(), Locations = context.Locations.Where(x => BigsSchool.Contains(x.LocationName)).ToList(), ServiceTypeId = serviceType["Mentoring Services"], ServiceRecipients = new List<ServiceRecipient>() },
                new Service { ServiceName = "School Attendance Mediation", ServiceProviders = context.ServiceProviders.Where(x => x.OrganizationName == "Northern Community Mediation").ToList(), Locations = context.Locations.Where(x => Mediation.Contains(x.County.Name)).ToList(), ServiceTypeId = serviceType["Behavioral Services"], ServiceRecipients = new List<ServiceRecipient>() },
                new Service { ServiceName = "Incorrigible Student Mediation", ServiceProviders = context.ServiceProviders.Where(x => x.OrganizationName == "Northern Community Mediation").ToList(), Locations = context.Locations.Where(x => Mediation.Contains(x.County.Name)).ToList(), ServiceTypeId = serviceType["Behavioral Services"], ServiceRecipients = new List<ServiceRecipient>() },
                new Service { ServiceName = "Child Abuse and Neglect Mediation", ServiceProviders = context.ServiceProviders.Where(x => x.OrganizationName == "Northern Community Mediation").ToList(), Locations = context.Locations.Where(x => Mediation.Contains(x.County.Name)).ToList(), ServiceTypeId = serviceType["Behavioral Services"], ServiceRecipients = new List<ServiceRecipient>() },
                new Service { ServiceName = "First-Time Shoplifter Mediation", ServiceProviders = context.ServiceProviders.Where(x => x.OrganizationName == "Northern Community Mediation").ToList(), Locations = context.Locations.Where(x => Mediation.Contains(x.County.Name)).ToList(), ServiceTypeId = serviceType["Behavioral Services"], ServiceRecipients = new List<ServiceRecipient>() },
                new Service { ServiceName = "Just the Facts – Violence Prevention Program", ServiceProviders = context.ServiceProviders.Where(x => x.OrganizationName == "Women's Resource Center of N. Michigan").ToList(), Locations = context.Locations.Where(x => Womens.Contains(x.County.Name)).ToList(), ServiceTypeId = serviceType["Educational Services"], ServiceRecipients = new List<ServiceRecipient>() },
                new Service { ServiceName = "Coaching Boys Into Men", ServiceProviders = context.ServiceProviders.Where(x => x.OrganizationName == "Women's Resource Center of N. Michigan").ToList(), Locations = context.Locations.Where(x => Womens.Contains(x.County.Name)).ToList(), ServiceTypeId = serviceType["Educational Services"], ServiceRecipients = new List<ServiceRecipient>() },
                new Service { ServiceName = "White Ribbon Basketball Games", ServiceProviders = context.ServiceProviders.Where(x => x.OrganizationName == "Women's Resource Center of N. Michigan").ToList(), Locations = context.Locations.Where(x => Womens.Contains(x.County.Name)).ToList(), ServiceTypeId = serviceType["Recreation and Enrichment"], ServiceRecipients = new List<ServiceRecipient>() },
                new Service { ServiceName = "Classroom Training – Teachers", ServiceProviders = context.ServiceProviders.Where(x => x.OrganizationName == "Women's Resource Center of N. Michigan").ToList(), Locations = context.Locations.Where(x => Womens.Contains(x.County.Name)).ToList(), ServiceTypeId = serviceType["Educational Services"], ServiceRecipients = new List<ServiceRecipient>() },
                new Service { ServiceName = "Anti-Bullying", ServiceProviders = context.ServiceProviders.Where(x => x.OrganizationName == "Women's Resource Center of N. Michigan").ToList(), Locations = context.Locations.Where(x => Womens.Contains(x.County.Name)).ToList(), ServiceTypeId = serviceType["Behavioral Services"], ServiceRecipients = new List<ServiceRecipient>() },
                new Service { ServiceName = "Healthy Relationships", ServiceProviders = context.ServiceProviders.Where(x => x.OrganizationName == "Women's Resource Center of N. Michigan").ToList(), Locations = context.Locations.Where(x => Womens.Contains(x.County.Name)).ToList(), ServiceTypeId = serviceType["Educational Services"], ServiceRecipients = new List<ServiceRecipient>() },
                new Service { ServiceName = "Before and After School Programs - Children's Learning Center", ServiceProviders = context.ServiceProviders.Where(x => x.OrganizationName == "Women's Resource Center of N. Michigan").ToList(), Locations = context.Locations.Where(x => Womens.Contains(x.County.Name)).ToList(), ServiceTypeId = serviceType["Child Care"], ServiceRecipients = new List<ServiceRecipient>() },

                new Service { ServiceName = "Infant Health (MIHP)", ServiceProviders = context.ServiceProviders.Where(x => x.OrganizationName == "Health Dept. of NW Michigan").ToList(), Locations = context.Locations.Where(x => MIHP.Contains(x.City.Name)).ToList(), ServiceTypeId = 2, ServiceRecipients = new List<ServiceRecipient>() },
                new Service { ServiceName = "Sexual Health", ServiceProviders = context.ServiceProviders.Where(x => x.OrganizationName == "Health Dept. of NW Michigan").ToList(), Locations = context.Locations.Where(x => SexHealth.Contains(x.City.Name)).ToList(), ServiceTypeId = 2, ServiceRecipients = new List<ServiceRecipient>() },
                new Service { ServiceName = "Dental Clinic", ServiceProviders = context.ServiceProviders.Where(x => x.OrganizationName == "Health Dept. of NW Michigan").ToList(), Locations = context.Locations.Where(x => Dental.Contains(x.City.Name)).ToList(), ServiceTypeId = 2, ServiceRecipients = new List<ServiceRecipient>() },
                new Service { ServiceName = "Reduced Cost Insurance (MI Child + Healthy Kids)", ServiceProviders = context.ServiceProviders.Where(x => x.OrganizationName == "Health Dept. of NW Michigan").ToList(), Locations = context.Locations.Where(x => HealthOther.Contains(x.City.Name)).ToList(), ServiceTypeId = 2, ServiceRecipients = new List<ServiceRecipient>() }

            );



        }
    }
}
