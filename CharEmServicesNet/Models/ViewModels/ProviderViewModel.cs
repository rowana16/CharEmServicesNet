using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CharEmServicesNet.Models.ViewModels
{
    public class IndexProviderViewModel
    {
        public IndexProviderViewModel()
        {
            ServiceProviders = new List<IndexItem>();
        }

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
        public CreateProviderViewModel()
        {
            this.SelectedLocations = new List<string>();
        }

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
        public MultiSelectList Locations { get; set; }
        public List<string> SelectedLocations { get; set; }

        public int AddressId { get; set; }
        public int OrganizationTypeId { get; set; }
        public string UserId { get; set; }
        public int? ProviderId { get; set; }
    }

    public class EditProviderViewModel: CreateProviderViewModel
    {
        public EditProviderViewModel()
        {
            this.SelectedAddLocations = new List<string>();
        }
        public MultiSelectList AddOtherLocations { get; set; }
        public List<string> SelectedAddLocations { get; set; }
    }
}