using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static CharEmServicesNet.Models.IRepository;

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
            CurrentCity = location.City;
            CurrentCounty = location.County;
        }
        public int Id { get; set; }

        [Display (Name = "Location Name")]
        [Required (ErrorMessage = "Location Name is Required")]
        public string LocationName { get; set; }
        public string LocationDescription { get; set; }
        public City CurrentCity { get; set; }
        public County CurrentCounty { get; set; }       

    }

    public class LocationCreateViewModel : LocationOperationViewModel
    {
        private ILocationRepository locationRepo;
        public LocationCreateViewModel(ApplicationDbContext _db)
        {
            this.CitiesSelect = GetSelectList(_db.Cities.ToList<ICityCounty>());
            this.CountiesSelect = GetSelectList(_db.Counties.ToList<ICityCounty>());
        }


        public string SelectedCity { get; set; }
        public string SelectedCounty { get; set; }
        public List<SelectListItem> CitiesSelect { get; set; }
        public List<SelectListItem> CountiesSelect { get; set; }

        private List<SelectListItem> GetSelectList(List<ICityCounty> data)
        {
            var result = new List<SelectListItem>();
            foreach(var item in data)
            {
                var selectItem = new SelectListItem()
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                };
                result.Add(selectItem);
            }
            return result;
        }
    }

   


}