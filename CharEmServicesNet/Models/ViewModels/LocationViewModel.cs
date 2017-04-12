﻿using System;
using System.Collections.Generic;
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
        public string LocationName { get; set; }
        public string LocationDescription { get; set; }
    }
}