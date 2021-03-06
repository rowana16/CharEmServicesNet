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

    public class ProviderBaseViewModel
    {
        public ProviderBaseViewModel()
        {
            this.SelectedLocations = new List<string>();            
            this.States = new SelectList(StateList.States.OrderBy(x => x.Text), "Value", "Text", State);
        }
        
        public virtual string OrganizationName { get; set; }
        public virtual string Address1 { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string City { get; set; }
        public SelectList States { get; set; }
        public virtual string State { get; set; }
        public virtual string Zip { get; set; }        
        public virtual string Description { get; set; }

        public MultiSelectList Locations { get; set; }        
        public virtual List<string> SelectedLocations { get; set; }

        public int AddressId { get; set; }

        public SelectList ProviderRep { get; set; }        
        public virtual string UserId { get; set; }

        public ApplicationUser CurrentRepresentative { get; set; }
        public virtual int? ProviderId { get; set; }
    }

    public class CreateProviderViewModel : ProviderBaseViewModel
    {
        

        [Display(Name = "Provider name")]
        [Required(ErrorMessage = "Organization Name Is Required")]       
        public override string OrganizationName { get; set; }

        [Required(ErrorMessage = "Address Is Required")]
        [Display(Name = "Address 1")]
        public override string Address1 { get; set; }
        
        [Display(Name = "Address 2")]
        public override string Address2 { get; set; }

        [Required(ErrorMessage ="City Is Required")]
        public override string City { get; set; }
       
        [Required (ErrorMessage ="State Is Required")]
        public override string State { get; set; }

        [Required(ErrorMessage = "Zip Is Required")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Zip")]
        public override string Zip { get; set; }  

        [Required(ErrorMessage = "Choose At Least One Location")]
        public override List<string> SelectedLocations { get; set; }
       
        [Required(ErrorMessage = "Representative Is Required")]
        public override string UserId { get; set; }       
    }

    public class EditProviderViewModel: ProviderBaseViewModel
    {
       

        [Display(Name = "Provider name")]
        [Required(ErrorMessage = "Organization Name Is Required")]       
        public override string OrganizationName { get; set; }

        [Required(ErrorMessage = "Address Is Required")]
        [Display(Name = "Address 1")]
        public override string Address1 { get; set; }
        
        [Display(Name = "Address 2")]
        public override string Address2 { get; set; }

        [Required(ErrorMessage ="City Is Required")]
        public override string City { get; set; }
       
        [Required (ErrorMessage ="State Is Required")]
        public override string State { get; set; }

        [Required(ErrorMessage = "Zip Is Required")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Zip")]
        public override string Zip { get; set; }        
       
        public MultiSelectList AddOtherLocations { get; set; }
        
        public string EditState { get; set; }
        public string ChangeUser { get; set; }
    }

    public class StateList
    {
        public static List<SelectListItem> States = new List<SelectListItem>()
        {           
            new SelectListItem() { Text="Alabama", Value="AL"},
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