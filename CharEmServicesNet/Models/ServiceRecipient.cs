﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CharEmServicesNet.Models
{
    public class ServiceRecipient
    {
        public ServiceRecipient()
        {
            this.Locations = new HashSet<Location>();
            this.Services = new HashSet<Service>();
            this.Users = new HashSet<ApplicationUser>();
        }

        public int Id { get; set; }
        public string OrganizationName { get; set; }

        public int AddressId { get; set; }
        public string Description { get; set; }
        public int OrganizationTypeId { get; set; }
        public string UserId { get; set; }
        

        public virtual Address Address { get; set; }
        public virtual OrganizationType OrganizationType { get; set; }
        public virtual ApplicationUser User { get; set; }
        

        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<Service> Services { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}