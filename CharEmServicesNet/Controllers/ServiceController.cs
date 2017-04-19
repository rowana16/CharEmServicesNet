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
        private IServiceRepository serviceRepo;
        private IServiceProviderRepository providerRepo;
        private IServiceRecipientRepository recipientRepo;
        private IServiceTypeRepository serviceTypeRepo;
        

        public ServiceController()
        {
            this.serviceRepo = new EFServiceRepository(_db);
            this.providerRepo = new EFProviderRepository(_db);
            this.recipientRepo = new EFRecipientRepository(_db);
            this.serviceTypeRepo = new EFServiceTypeRepository(_db);
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
            model.ServiceType = serviceTypeRepo.ResultTable
                .Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() })
                .ToList();   
            model.Providers = providerRepo.ResultTable
                .Select(x => new SelectListItem() { Text = x.OrganizationName, Value = x.Id.ToString() })
                .ToList();
            model.Recipients = recipientRepo.ResultTable
                .Select(x => new SelectListItem() { Text = x.OrganizationName, Value = x.Id.ToString() })
                .ToList();

            return View(model);
        }

        // POST: Service/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "UnitedWayAdmin")]
        public ActionResult Create(ServiceOperationViewModel model)
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
        public ActionResult Edit(ServiceOperationViewModel model)
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
           

            foreach (var provider in providers)
            {
                var listItem = new SelectListItem();
                listItem.Text = provider.OrganizationName;
                listItem.Value = provider.Id.ToString();
                providerList.Add(listItem);              
            }
           

            var model = new ServiceEditViewModel()
            {
                Id = service.Id,
                SelectedServiceTypeId = service.ServiceTypeId,
                ServiceName = service.ServiceName,
                ServiceDetails = service.ServiceDetails,
                Providers = providerList,                
                CurrentProvider = service.ServiceProviders.First()
            };

            return model;
        }

        private Service GetServiceFromViewModel (ServiceOperationViewModel model)
        {
            var providerId = Convert.ToInt32(model.SelectedProviderId);
            var service = new Service();
            if (model.Id != 0)
            {
                service = serviceRepo.ResultTable.Where(x => x.Id == model.Id).First();
            }
            
            service.Id = model.Id;
            service.ServiceName = model.ServiceName;
            service.ServiceDetails = model.ServiceDetails;

            service.ServiceTypeId = 1;
            service.ServiceType = serviceTypeRepo.ResultTable
                .Where(x => x.Id == model.SelectedServiceTypeId)
                .FirstOrDefault();
            service.ServiceRecipients = recipientRepo.ResultTable
                .Where(x => x.Id == model.SelectedRecipientId)
                .ToList();

            if(service.ServiceProviders.Any(x=>x.Id != providerId))
            {
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
            
            

            return service;
        }


        private ServiceOperationViewModel SaveServiceFromViewModel (ServiceOperationViewModel model)
        {
            var providerId = Convert.ToInt32(model.SelectedProviderId);
            var service = new Service();
            service.ServiceName = model.ServiceName;
            service.ServiceDetails = model.ServiceDetails;

            service.ServiceTypeId = model.SelectedServiceTypeId;
            service.ServiceType = _db.ServiceTypes.Find(model.SelectedServiceTypeId);
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
