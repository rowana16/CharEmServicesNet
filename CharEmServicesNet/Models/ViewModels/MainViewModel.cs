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
        public MainViewModel(List<Location> locations, List<City> cities, List<County> counties)
        {
            locationList = new List<CheckModel>();
            foreach (Location currLocation in locations)
            {
                var checkItem = new CheckModel()
                {
                    Id = currLocation.Id,
                    Name = currLocation.LocationName,
                    Checked = false
                };
                this.locationList.Add(checkItem);
            }
            
        }
        
        public string currentId { get; set; }
        public ApplicationUser currentUser { get; set; }           
        public bool IsAdmin { get; set; }
        public bool IsProvider { get; set; }
        public int ProviderId { get; set; }

        public List<CheckModel> locationList { get; set; }
        [Display(Name = "Available Locations")]
        public List<string> selectedLocations { get; set; }
    }   


    
    public class LocationPartialViewModel
    {
        public LocationPartialViewModel(List<Service> servicesToAdd)
        {
            this.services = servicesToAdd;            
        }

        public List<Service> services { get; set; }

    } 

    public class CheckModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
    }
}