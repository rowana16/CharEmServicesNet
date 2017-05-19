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
            CityList = new List<SelectListItem>();            
            locationList = new List<SelectListItem>();
            CountyList = new List<SelectListItem>();
            SchoolList = new List<SelectListItem>();
            nullItem = new SelectListItem();// { Text = " ", Value = " " };
            var schools = locations.Where(x => x.IsSchool == true).ToList();
            CountyList.Add(nullItem);
            CityList.Add(nullItem);
            SchoolList.Add(nullItem);

            foreach (Location currLocation in locations)
            {
                var checkItem = new SelectListItem()
                {
                    Value = currLocation.Id.ToString(),
                    Text = currLocation.LocationName                    
                };
                this.locationList.Add(checkItem);
            }
            locationList.Add(nullItem) ;
            locationList.OrderBy(x => x.Text);

            foreach (County currCounty in counties)
            {
                var checkItem = new SelectListItem()
                {
                    Value = currCounty.Id.ToString(),
                    Text = currCounty.Name                    
                };
                this.CountyList.Add(checkItem);
            }            
            CountyList.OrderBy(x => x.Text);


            foreach (City currCity in cities)
            {
                var checkItem = new SelectListItem()
                {
                    Value = currCity.Id.ToString(),
                    Text = currCity.Name
                    
                };
                this.CityList.Add(checkItem);
            }            
            CityList.OrderByDescending(x => x.Text);


            foreach (Location currSchool in schools)
            {
                var checkItem = new SelectListItem()
                {
                    Value = currSchool.Id.ToString(),
                    Text = currSchool.LocationName                    
                };
                this.SchoolList.Add(checkItem);
            }            
            SchoolList.OrderBy(x => x.Text);


        }

        public string currentId { get; set; }
        public ApplicationUser currentUser { get; set; }           
        public bool IsAdmin { get; set; }
        public bool IsProvider { get; set; }
        public int ProviderId { get; set; }
        private SelectListItem nullItem { get; set; }

        public List<SelectListItem> locationList { get; set; }
        [Display(Name = "Available Locations")]
        public List<string> selectedLocations { get; set; }

        public List<SelectListItem> CityList { get; set; }
        public List<SelectListItem> CountyList { get; set; }
        public List<SelectListItem> SchoolList { get; set; }

        public string selectedCity { get; set; }
        public string selectedCounty { get; set; }
        public string selectedSchool { get; set; }
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