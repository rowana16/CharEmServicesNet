using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CharEmServicesNet.Models.ViewModels
{
    public class IndexProviderViewModel
    {
        public List<IndexItem> ServiceProviders { get; set; }
    }

    public class IndexItem
    {
        public int ProviderId { get; set; }
        public string OrganizationName { get; set; }
        public string City { get; set; }
    }

    public class CreateProviderViewModel
    {
        [Display(Name = "Provider name")]
        public string OrganizationName { get; set; }
        [Display(Name = "Address 1")]
        public string Address1 { get; set; }
        [Display(Name = "Address 2")]
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Description { get; set; }
    }
}