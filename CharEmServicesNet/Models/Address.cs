using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CharEmServicesNet.Models
{
    public class Address
    {
        public Address()
        {
            this.ServiceProviders = new HashSet<ServiceProvider>();
            this.ServiceRecipients = new HashSet<ServiceRecipient>();
            this.Users = new HashSet<ApplicationUser>();
        }

        public int Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public virtual ICollection<ServiceProvider> ServiceProviders { get; set; }
        public virtual ICollection<ServiceRecipient> ServiceRecipients { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}