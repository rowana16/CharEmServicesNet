using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CharEmServicesNet.Models
{
    public class Location
    {
        public Location()
        {
            this.ServiceProviders = new HashSet<ServiceProvider>();
            this.ServiceRecipients = new HashSet<ServiceRecipient>();
            this.Services = new HashSet<Service>();
        }

        public int Id { get; set; }
        public string LocationName { get; set; }
        public string LocationDescription { get; set; }

        public int? CityId { get; set; }
        public int CountyId { get; set; }

        public virtual City City { get; set; }
        public virtual County County { get; set; }

        public virtual ICollection<ServiceProvider> ServiceProviders { get; set; }
        public virtual ICollection<ServiceRecipient> ServiceRecipients { get; set; }
        public virtual ICollection<Service> Services { get; set; }
    }
}