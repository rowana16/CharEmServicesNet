﻿using System;
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
            this.States = new SelectList(StateList.States.OrderBy(x => x.Text), "Value", "Text", State);
        }

        [Display(Name = "Provider name")]
        [Required]       
        public string OrganizationName { get; set; }
        [Required]
        [Display(Name = "Address 1")]
        public string Address1 { get; set; }
        [Required]
        [Display(Name = "Address 2")]
        public string Address2 { get; set; }
        [Required]
        public string City { get; set; }
        public SelectList States;
        [Required]
        public string State { get; set; }
        [Required]
        public string Zip { get; set; }
        public string Description { get; set; }
        public MultiSelectList Locations { get; set; }
        [Required]
        public List<string> SelectedLocations { get; set; }

        public int AddressId { get; set; }
        public int OrganizationTypeId { get; set; }        
       
        public SelectList ProviderRep { get; set;}
        public string UserId { get; set; }
        public int? ProviderId { get; set; }
        public ApplicationUser CurrentRepresentative { get; set; }
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

    public class StateList
    {
        public static List<SelectListItem> States = new List<SelectListItem>()
        {
            new SelectListItem() {Text="Alabama", Value="AL"},
            new SelectListItem() { Text="Alaska", Value="AK"},
            new SelectListItem() { Text="Arizona", Value="AZ"},
            new SelectListItem() { Text="Arkansas", Value="AR"},
            new SelectListItem() { Text="California", Value="CA"},
            new SelectListItem() { Text="Colorado", Value="CO"},
            new SelectListItem() { Text="Connecticut", Value="CT"},
            new SelectListItem() { Text="District of Columbia", Value="DC"},
            new SelectListItem() { Text="Delaware", Value="DE"},
            new SelectListItem() { Text="Florida", Value="FL"},
            new SelectListItem() { Text="Georgia", Value="GA"},
            new SelectListItem() { Text="Hawaii", Value="HI"},
            new SelectListItem() { Text="Idaho", Value="ID"},
            new SelectListItem() { Text="Illinois", Value="IL"},
            new SelectListItem() { Text="Indiana", Value="IN"},
            new SelectListItem() { Text="Iowa", Value="IA"},
            new SelectListItem() { Text="Kansas", Value="KS"},
            new SelectListItem() { Text="Kentucky", Value="KY"},
            new SelectListItem() { Text="Louisiana", Value="LA"},
            new SelectListItem() { Text="Maine", Value="ME"},
            new SelectListItem() { Text="Maryland", Value="MD"},
            new SelectListItem() { Text="Massachusetts", Value="MA"},
            new SelectListItem() { Text="Michigan", Value="MI"},
            new SelectListItem() { Text="Minnesota", Value="MN"},
            new SelectListItem() { Text="Mississippi", Value="MS"},
            new SelectListItem() { Text="Missouri", Value="MO"},
            new SelectListItem() { Text="Montana", Value="MT"},
            new SelectListItem() { Text="Nebraska", Value="NE"},
            new SelectListItem() { Text="Nevada", Value="NV"},
            new SelectListItem() { Text="New Hampshire", Value="NH"},
            new SelectListItem() { Text="New Jersey", Value="NJ"},
            new SelectListItem() { Text="New Mexico", Value="NM"},
            new SelectListItem() { Text="New York", Value="NY"},
            new SelectListItem() { Text="North Carolina", Value="NC"},
            new SelectListItem() { Text="North Dakota", Value="ND"},
            new SelectListItem() { Text="Ohio", Value="OH"},
            new SelectListItem() { Text="Oklahoma", Value="OK"},
            new SelectListItem() { Text="Oregon", Value="OR"},
            new SelectListItem() { Text="Pennsylvania", Value="PA"},
            new SelectListItem() { Text="Rhode Island", Value="RI"},
            new SelectListItem() { Text="South Carolina", Value="SC"},
            new SelectListItem() { Text="South Dakota", Value="SD"},
            new SelectListItem() { Text="Tennessee", Value="TN"},
            new SelectListItem() { Text="Texas", Value="TX"},
            new SelectListItem() { Text="Utah", Value="UT"},
            new SelectListItem() { Text="Vermont", Value="VT"},
            new SelectListItem() { Text="Virginia", Value="VA"},
            new SelectListItem() { Text="Washington", Value="WA"},
            new SelectListItem() { Text="West Virginia", Value="WV"},
            new SelectListItem() { Text="Wisconsin", Value="WI"},
            new SelectListItem() { Text="Wyoming", Value="WY"}
        };
    }
}