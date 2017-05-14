using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CharEmServicesNet.Models.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel(List<Location> locations)
        {
            locationList = new List<SelectListItem>();
            foreach (Location currLocation in locations)
            {
                this.locationList.Add(new SelectListItem() { Text = currLocation.LocationName, Value = currLocation.Id.ToString() });
            }
        }
        
        public string currentId { get; set; }
        public ApplicationUser currentUser { get; set; }
        public List<SelectListItem> locationList { get; set; }
        [Display(Name = "Available Locations")]
        public List<string> selectedLocations { get; set; }        
        public bool IsAdmin { get; set; }
    }   
    
    public class LocationPartialViewModel
    {
        public LocationPartialViewModel(List<Service> servicesToAdd)
        {
            this.services = servicesToAdd;            
        }

        public List<Service> services { get; set; }

    } 
}