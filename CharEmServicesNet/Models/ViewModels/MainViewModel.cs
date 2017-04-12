﻿using System;
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
        public string selectedLocation { get; set; }
        public IList<string> currentRoles { get; set; }
    }   
    
    public class LocationPartialViewModel
    {
        public LocationPartialViewModel(IList<ServiceProvider> providers)
        {
            this.services = new List<Service>();
            foreach (var provider in providers)
            {
                foreach( var service in provider.Services)
                {
                    services.Add(service);
                }
            }
        }

        public List<Service> services { get; set; }

    } 
}