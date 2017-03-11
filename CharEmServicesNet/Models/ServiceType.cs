using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CharEmServicesNet.Models
{
    public class ServiceType
    {
        public ServiceType()
        {
            this.Services = new HashSet<Service>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Service> Services { get; set; }
    }
}