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
        public MainViewModel(List<ServiceType> serviceTypes)
        {
            serviceTypeItem = serviceTypes;
        }

        public string currentId { get; set; }
        public ApplicationUser currentUser { get; set; }           
        public bool IsAdmin { get; set; }
        public bool IsProvider { get; set; }
        public int ProviderId { get; set; }
        
        public List<ServiceType> serviceTypeItem { get; set; }                
    }   


    
    public class LocationPartialViewModel
    {
        public LocationPartialViewModel(List<Location> locationsIn)
        {
            locations = locationsIn;                        
        }

        public List<Location> locations { get; set; }
      
    } 

    public class ServicePartialViewModel
    {
        public List<Service> _services;

        public ServicePartialViewModel(List<Service> services)
        {
            _services = services;
        }



    }

    
}