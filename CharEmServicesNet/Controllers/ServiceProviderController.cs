using CharEmServicesNet.Models;
using CharEmServicesNet.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using static CharEmServicesNet.Models.IRepository;

namespace CharEmServicesNet.Controllers
{
    [Authorize(Roles = "UnitedWayAdmin, ServiceProvider")]
    public class ServiceProviderController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        private IServiceProviderRepository providerRepo;
        private IAddressRepository addressRepo;
        private ILocationRepository locationRepo;
        private IUserRepository userRepo;


        public ServiceProviderController()
        {
            this.providerRepo = new EFProviderRepository(_db);
            this.addressRepo = new EFAddressRepository(_db);
            this.locationRepo = new EFLocationRepository(_db);
            this.userRepo = new EFUserRepository(_db);
        }

        // GET: ServiceProvider
        public ActionResult Index()
        {
            var model = new IndexProviderViewModel();
            var names = new List<string>();
            var cities = new List<string>();

            var providers = providerRepo.ResultTable.ToList();
            var addresses = addressRepo.ResultTable.ToList();

            foreach (var provider in providers)
            {
                var currItem = new IndexItem();
                currItem.ProviderId = provider.Id;
                currItem.OrganizationName = provider.OrganizationName;
                var currAddress = addresses.Where(a => a.Id == provider.AddressId);
                currItem.City = currAddress.First().City;
                model.ServiceProviders.Add(currItem);
            }

            return View(model);
        }

        // GET: ServiceProvider/Details/5
        public ActionResult Details(int id)
        {
            var model = GetModelWithProviderId(id);
            return View(model);
        }

        // GET: ServiceProvider/Create
        public ActionResult Create()
        {            
            var locations = locationRepo.ResultTable.ToList();
            var locationSelectList = GetLocationList(locations);
            var users = userRepo.ResultTable.ToList();
            var userSelectList = GetUserList(users);
            
            var model = new CreateProviderViewModel();
            model.Locations = new MultiSelectList(locationSelectList.OrderBy(x => x.Text)
                    , "Value", "Text", model.SelectedLocations);
            model.ProviderRep = new SelectList(userSelectList.OrderBy(x => x.Text)
                    , "Value", "Text", model.UserId);

            return View(model);
        }

        // POST: ServiceProvider/Create
        [HttpPost]
        public ActionResult Create(EditProviderViewModel model)
        {
            var newProvider = new ServiceProvider();
            var newAddress = new Address();

            newAddress.Address1 = model.Address1;
            newAddress.Address2 = model.Address2;
            newAddress.City = model.City;
            newAddress.State = model.State;
            newAddress.Zip = model.Zip;

            newAddress = addressRepo.Save(newAddress);

            newProvider.AddressId = newAddress.Id;
            newProvider.OrganizationName = model.OrganizationName;
            newProvider.Description = model.Description;
            newProvider.OrganizationTypeId = 5;
            newProvider.UserId = model.UserId;
            newProvider.Locations = GetSelectedLocations(model.SelectedLocations);
            providerRepo.Save(newProvider);

            return RedirectToAction("Details", new { id = newProvider.Id });
        }

        // GET: ServiceProvider/Edit/5
        public ActionResult Edit(int id)
        {            
            var currProvider = providerRepo.ResultTable.Where(x => x.Id == id).First();
            var currUser = userRepo.ResultTable.Where(x => x.Id == currProvider.UserId).First();
            var locations = currProvider.Locations;
            var locationSelectList = GetLocationList(locations.ToList());
            var locationAddList = GetOtherLocations(locations.ToList());
            var userList = userRepo.ResultTable.ToList();
            var userSelectList = GetUserList(userList);
            var model = new EditProviderViewModel()
            {
                ProviderId = id,
                OrganizationName = currProvider.OrganizationName,
                Address1 = currProvider.Address.Address1,
                Address2 = currProvider.Address.Address2,
                City = currProvider.Address.City,
                State = currProvider.Address.State,
                Zip = currProvider.Address.Zip,
                Description = currProvider.Description,
                CurrentRepresentative = currUser,
                UserId = currUser.Id
             };

            model.Locations = new MultiSelectList(locationSelectList.OrderBy(x => x.Text)
                , "Value", "Text", model.SelectedLocations);
            model.AddOtherLocations = new MultiSelectList(locationAddList.OrderBy(x => x.Text)
                , "Value", "Text", model.SelectedAddLocations);

            model.ProviderRep = new SelectList(userSelectList.OrderBy(x => x.Text)
                , "Value", "Text", model.UserId);



            return View(model);
        }

        // POST: ServiceProvider/Edit/5
        [HttpPost]
        public ActionResult Edit(EditProviderViewModel model)
        {

            var currProvider = providerRepo.ResultTable.Where(i => i.Id == model.ProviderId).First();
            var currAddress = addressRepo.ResultTable.Where(i => i.Id == currProvider.AddressId).First();

            currAddress.Address1 = model.Address1;
            currAddress.Address2 = model.Address2;
            currAddress.City = model.City;
            currAddress.State = model.State;
            currAddress.Zip = model.Zip;

            addressRepo.Save(currAddress);

            currProvider.OrganizationName = model.OrganizationName;
            currProvider.Description = model.Description;
            currProvider.Locations = EditSelectedLocations(model.SelectedLocations, model.SelectedAddLocations, model.ProviderId);
            currProvider.UserId = model.UserId;
            if (model.ChangeUser != null)
            {
                currProvider.UserId = model.ChangeUser;
            }
            providerRepo.Save(currProvider);            

            return RedirectToAction("Details", new { id = model.ProviderId });

        }

        // GET: ServiceProvider/Delete/5
        public ActionResult Delete(int id)
        {

            return View(GetModelWithProviderId(id));
        }

        // POST: ServiceProvider/Delete/5
        [HttpPost]
        public ActionResult Delete(CreateProviderViewModel model)
        {
            var delProvider = providerRepo.ResultTable.Where(i => i.Id == model.ProviderId).First();
            var delAddress = addressRepo.ResultTable.Where(i => i.Id == delProvider.AddressId).First();
            providerRepo.Delete(delProvider);
            addressRepo.Delete(delAddress);

            return RedirectToAction("Index");
        }

        private EditProviderViewModel GetModelWithProviderId(int providerId)
        {
            var currProvider = providerRepo.ResultTable.Where(i => i.Id == providerId).First();
            var currAddress = addressRepo.ResultTable.Where(i => i.Id == currProvider.AddressId).First();
            var currLocations = currProvider.Locations;
            var locationSelectList = GetLocationList(currLocations.ToList());
            var otherLocations = GetOtherLocations(currLocations.ToList());
            var userSelectList = new List<SelectListItem>();

           
            var users = userRepo.ResultTable.ToList();
            userSelectList = GetUserList(users);

            var model = new EditProviderViewModel();
            model.Address1 = currAddress.Address1;
            model.Address2 = currAddress.Address2;
            model.City = currAddress.City;
            model.State = currAddress.State;
            model.Zip = currAddress.Zip;
            model.OrganizationName = currProvider.OrganizationName;
            model.Description = currProvider.Description;
            model.AddressId = currAddress.Id;
            model.OrganizationTypeId = currProvider.OrganizationTypeId;
            model.UserId = currProvider.UserId;
            model.ProviderId = providerId;            
            if (currProvider.UserId != null)
            {
                model.CurrentRepresentative = userRepo.ResultTable.Where(x => x.Id == currProvider.UserId).First();
            }
            model.Locations = new MultiSelectList(locationSelectList.OrderBy(x => x.Text)
                   , "Value", "Text", model.SelectedLocations);
            model.AddOtherLocations = new MultiSelectList(otherLocations.OrderBy(x=> x.Text)
                    ,"Value", "Text", model.SelectedAddLocations);           
            model.ProviderRep = new SelectList(userSelectList.OrderBy(x => x.Text)
                   , "Value", "Text", model.UserId);

            return model;
        }

        List<Location> GetSelectedLocations(List<string> selectedLocations)
        {
            var locationList = new List<Location>();

            foreach (var location in selectedLocations)
            {
                int locationId = Convert.ToInt32(location);
                var selectedLocation = locationRepo.ResultTable
                    .Where(x => x.Id == locationId).First();
                locationList.Add(selectedLocation);
            }

            return locationList;
        }

        List<Location> EditSelectedLocations(List<string> removeLocations, List<string> addLocations, int? providerId)
        {
            var currProvider = providerRepo.ResultTable.Where(x => x.Id == providerId).First();
            var currLocationIds = currProvider.Locations.Select(x=>x.Id).ToList();
            var removeLocationIds = GetLocationIds(removeLocations);
            var addLocationIds = GetLocationIds(addLocations);

            var newLocationList = new List<Location>();
            var newLocationIdList = new List<int>();

            foreach (int currId in currLocationIds)
            {
                bool found = false;
                foreach(int removeId in removeLocationIds)
                {
                    if (removeId == currId)
                    {
                        found = true;
                    }
                }
                if (found == false)
                {
                    newLocationIdList.Add(currId);
                }
            }

            foreach(int addId in addLocationIds)
            {
                newLocationIdList.Add(addId);
            }

            newLocationList = GetLocationsFromIds(newLocationIdList);

            return newLocationList;
        }

        private List<Location> GetLocationsFromIds(List<int> newLocationIdList)
        {
            List<Location> newLocationList = new List<Location>();
            foreach (int id in newLocationIdList)
            {
                newLocationList.Add(locationRepo.ResultTable.Where(x => x.Id == id).First());
            }
            return newLocationList;
        }

        List<int> GetLocationIds(List<string> locations)
        {
            var locationIds = new List<int>();
            foreach(string id in locations)
            {
                locationIds.Add(Convert.ToInt32(id));
            }
            return locationIds;
        }

        List<SelectListItem> GetLocationList(List<Location> locations)
        {
            var locationList = new List<SelectListItem>();
            foreach (var location in locations)
            {
                var item = new SelectListItem
                {
                    Value = location.Id.ToString(),
                    Text = location.LocationName
                };
                locationList.Add(item);
            }
            return locationList;
        }

        List<SelectListItem> GetUserList(List<ApplicationUser> users)
        {
            var list = new List<SelectListItem>();
            foreach (var user in users)
            {
                var item = new SelectListItem()
                {
                    Value = user.Id.ToString(),
                    Text = user.DisplayName
                };
                list.Add(item);
            }
            return list;
        }

        List<SelectListItem> GetOtherLocations(List<Location> locations)
        {
            var locationList = new List<SelectListItem>();
            var allLocations = locationRepo.ResultTable.ToList();

            foreach(var location in allLocations)
            {
                var found = false;
                foreach( var currlocation in locations)
                {
                    if (location.Id == currlocation.Id)
                    {
                        found = true;
                    }
                }

                if (found == false)
                {
                    var item = new SelectListItem
                    {
                        Value = location.Id.ToString(),
                        Text = location.LocationName
                    };
                    locationList.Add(item);
                }

                found = false;
            }
            return locationList;
        }
    }
}
