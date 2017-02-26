using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CharEmServicesNet.Models
{
    public class Service
    {
        public Service()
        {
            this.ServiceProviders = new HashSet<ServiceProvider>();
            this.ServiceRecipients = new HashSet<ServiceRecipient>();
        }

        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDetails { get; set; }

        public int ServiceTypeId { get; set; }
        public virtual ServiceType ServiceType { get; set; }

        public virtual ICollection<ServiceProvider> ServiceProviders { get; set; }
        public virtual ICollection<ServiceRecipient> ServiceRecipients { get; set; }
    }
}