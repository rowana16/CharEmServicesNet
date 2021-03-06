﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CharEmServicesNet.Models
{
    public class City : ICityCounty
    {
        public City()
        {
            this.Locations = new HashSet<Location>();
        }

        public int Id { get; set; }
        public int CountyId { get; set; }
        public string Name { get; set; }

        public virtual County County { get; set; }

        public virtual ICollection<Location> Locations { get; set; }

    }

    public interface ICityCounty
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}