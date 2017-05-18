using CharEmServicesNet.Models;
using CharEmServicesNet.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static CharEmServicesNet.Models.IRepository;

namespace CharEmServicesNet.Controllers
{
    public class ServiceController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        private IGenericRepository<Service> serviceRepo;
        private IGenericRepository<ServiceProvider> providerRepo;
        private IGenericRepository<ServiceRecipient> recipientRepo;
        private IGenericRepository<Location> locationRepo;   
        

        public ServiceController()
        {
            this.serviceRepo = new EFServiceRepository(_db);
            this.providerRepo = new EFProviderRepository(_db);
            this.recipientRepo = new EFRecipientRepository(_db);
            this.locationRepo = new EFLocationRepository(_db);
        }

        // GET: Service
        public ActionResult Index()
        {
            var model = new ServiceIndexViewModel();

            model.services = serviceRepo.ResultTable.ToList();
            return View(model);
        }

        // GET: Service/Details/5
        public ActionResult Details(int id)
        {
            var model = GetModelWithId(id);
            
            return View(model);
        }

        // GET: Service/ExtendedDetails/5
        public ActionResult ExtendedDetails(int id)
        {
            var model = GetDetailWithId(id);
            return View(model);
        }

        // GET: Service/Create
        [Authorize(Roles = "UnitedWayAdmin")]
        public ActionResult Create()
        {            
            var model = new ServiceOperationViewModel();
             
            model.Providers = providerRepo.ResultTable
                .Select(x => new SelectListItem() { Text = x.OrganizationName, Value = x.Id.ToString() })
                .ToList();
            model.Recipients = recipientRepo.ResultTable
                .Select(x => new SelectListItem() { Text = x.OrganizationName, Value = x.Id.ToString() })
                .ToList();
            model.Locations = locationRepo.ResultTable
                .Select(x => new SelectListItem() { Text = x.LocationName, Value = x.Id.ToString() })
                .ToList();
            return View(model);
        }

        // POST: Service/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "UnitedWayAdmin")]
        public ActionResult Create(ServiceEditViewModel model)
        {
            var service = GetServiceFromViewModel(model);
            service = serviceRepo.Save(service);
           
            if (service.Id != 0)
            {
                return RedirectToAction("Details", new { id = service.Id });
            }

            model.Providers = providerRepo.ResultTable
               .Select(x => new SelectListItem() { Text = x.OrganizationName, Value = x.Id.ToString() })
               .ToList();
            model.Recipients = recipientRepo.ResultTable
                .Select(x => new SelectListItem() { Text = x.OrganizationName, Value = x.Id.ToString() })
                .ToList();
            return View(model);
           
        }

        // GET: Service/Edit/5
        [Authorize(Roles = "UnitedWayAdmin")]
        public ActionResult Edit(int id)
        {                              
           var service = GetModelWithId(id);
           return View(service);
        }

        // POST: Service/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "UnitedWayAdmin")]
        public ActionResult Edit(ServiceEditViewModel model)
        {
            var service = GetServiceFromViewModel(model);
            serviceRepo.Save(service);
            
            return RedirectToAction("Details", new { id = model.Id });
        }

        // GET: Service/Delete/5
        [Authorize(Roles = "UnitedWayAdmin")]
        public ActionResult Delete(int id)
        {
            var model = GetModelWithId(id);
            
            return View(model);
        }

        // POST: Service/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "UnitedWayAdmin")]
        public ActionResult Delete(ServiceOperationViewModel model)
        {
            var service = serviceRepo.ResultTable.Where(x => x.Id == model.Id).FirstOrDefault();
            serviceRepo.Delete(service);
            return RedirectToAction("Index");
        }

        private ServiceDetailViewModel GetDetailWithId(int id)
        {
            var service = serviceRepo.ResultTable.Where(x => x.Id == id).First();

            var model = new ServiceDetailViewModel()
            {
                Id = id,
                ServiceName = service.ServiceName,
                ServiceDetails = service.ServiceDetails,
                CurrentProvider = service.ServiceProviders.First()
            };
            return model;
        }

        private ServiceEditViewModel GetModelWithId(int id)
        {
            var service = serviceRepo.ResultTable.Where(x => x.Id == id).FirstOrDefault();
            var providers = providerRepo.ResultTable.ToList();        
            var providerList = new List<SelectListItem>();
            var locationList = new List<SelectListItem>();
            var missingLocationList = new List<SelectListItem>();
            var missingLocations = locationRepo.ResultTable.ToList();
            var nullSelectItem = new SelectListItem();
            providerList.Add(nullSelectItem);

            foreach (var provider in providers)
            {
                var listItem = new SelectListItem();
                listItem.Text = provider.OrganizationName;
                listItem.Value = provider.Id.ToString();
                providerList.Add(listItem);              
            }

            foreach(var location in service.Locations)
            {
                var listItem = new SelectListItem();
                listItem.Text = location.LocationName;
                listItem.Value = location.Id.ToString();
                locationList.Add(listItem);
                missingLocations.Remove(location);
            }

            foreach(var location in missingLocations)
            {
                var listItem = new SelectListItem();
                listItem.Text = location.LocationName;
                listItem.Value = location.Id.ToString();
                missingLocationList.Add(listItem);                
            }

            var model = new ServiceEditViewModel()
            {
                Id = service.Id,

                ServiceName = service.ServiceName,
                ServiceDetails = service.ServiceDetails,
                Providers = providerList,
                CurrentProvider = service.ServiceProviders.FirstOrDefault(),
                Locations = locationList,
                MissingLocations = missingLocationList
            };

            return model;
        }

        private Service GetServiceFromViewModel (ServiceEditViewModel model)
        {           
            var service = new Service();
            if (model.Id != 0)
            {
                service = serviceRepo.ResultTable.Where(x => x.Id == model.Id).First();
            }            
           
            service.ServiceName = model.ServiceName;
            service.ServiceDetails = model.ServiceDetails;
            var locationList = new List<string>();
            if(model.AddLocations.Count > 0)
            {
                locationList = model.AddLocations;                       
                for (int i = 0; i < model.AddLocations.Count; i++)
                {
                    var locationId = Convert.ToInt16(locationList[i]);
                    var location = locationRepo.ResultTable.Where(x => x.Id == locationId).FirstOrDefault();
                    service.Locations.Add(location);
                }
            }
            
            if(model.RemoveLocations.Count > 0)
            {
                locationList = model.RemoveLocations;
                for (int i = 0; i < model.RemoveLocations.Count; i++)
                {
                    var locationId = Convert.ToInt16(locationList[i]);
                    var location = locationRepo.ResultTable.Where(x => x.Id == locationId).FirstOrDefault();
                    service.Locations.Remove(location);
                }
            }
            

            service.ServiceRecipients = recipientRepo.ResultTable
                .Where(x => x.Id == model.SelectedRecipientId)
                .ToList();
            
            
            if(model.SelectedProviderId != null)
            {
                var providerId = Convert.ToInt32(model.SelectedProviderId);
                var initialProviderCount = service.ServiceProviders.Count;
                for (int i = 0; i < initialProviderCount; i++)
                {
                    var provider = service.ServiceProviders.Last();
                    service.ServiceProviders.Remove(provider);
                }                        
                var newServiceProvider = providerRepo.ResultTable
                    .Where(x => x.Id == providerId).First();
                service.ServiceProviders.Add(newServiceProvider);
            }
                
            



            service.ServiceType = new ServiceType();
            serviceRepo.Save(service);
            return service;
        }


        private ServiceOperationViewModel SaveServiceFromViewModel (ServiceOperationViewModel model)
        {
            var providerId = Convert.ToInt32(model.SelectedProviderId);
            var service = new Service();
            service.ServiceName = model.ServiceName;
            service.ServiceDetails = model.ServiceDetails;

            
            service.ServiceProviders = _db.ServiceProviders
                .Where(x => x.Id == providerId)
                .ToList();
            service.ServiceRecipients = _db.ServiceRecipients
                .Where(x => x.Id == model.SelectedRecipientId)
                .ToList();
            try
            {
                serviceRepo.Save(service);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine(ex.Message);

                return model;
            }

            model.Id = service.Id;
            return model;
           
        }
    }
}
