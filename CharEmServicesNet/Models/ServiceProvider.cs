using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharEmServicesNet.Models
{
    public class ServiceProvider
    {
        public ServiceProvider()
        {
            this.Locations = new HashSet<Location>();
            this.Services = new HashSet<Service>();
            this.Users = new HashSet<ApplicationUser>();
        }
        public int Id { get; set; }
        public string OrganizationName { get; set; }

        public int AddressId { get; set; }
        public int OrganizationTypeId { get; set; }
        public string UserId { get; set; }
        public int TeamId { get; set; }

        public virtual Address Address { get; set; }
        public virtual OrganizationType OrganizationType { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Team Team { get; set; }

        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<Service> Services { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }

    }
}
