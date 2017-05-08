using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CharEmServicesNet.Models.ViewModels
{
    public class LocationIndexViewModel
    {
        public List<Location> Locations { get; set; }

        public LocationIndexViewModel(List<Location> locationSet)
        {
            Locations = locationSet;
        }
    }

    public class LocationOperationViewModel
    {
        public LocationOperationViewModel()
        {
        }
        public LocationOperationViewModel(Location location)
        {
            Id = location.Id;
            LocationName = location.LocationName;
            LocationDescription = location.LocationDescription;
        }
        public int Id { get; set; }

        [Display (Name = "Location Name")]
        [Required (ErrorMessage = "Location Name is Required")]
        public string LocationName { get; set; }
        public string LocationDescription { get; set; }
    }


}