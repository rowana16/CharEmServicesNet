using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CharEmServicesNet.Models.ViewModels
{
    public class IndexAdminProviderViewModel
    {
        public List<IndexItem> ServiceProviders { get; set; }
    }

    public class IndexItem
    {
        public string OrganizationName { get; set; }
        public string City { get; set; }
    }

    public class CreateAdminProviderViewModel
    {
        [Display(Name = "Provider name")]
        public string OrganizationName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Description { get; set; }
    }
}