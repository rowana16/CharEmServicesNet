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

        // GET: Service/Create
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
        public ActionResult Edit(int id)
        {                              
           var service = GetModelWithId(id);
           return View(service);
        }

        // POST: Service/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ServiceOperationViewModel model)
        {
            var service = GetServiceFromViewModel(model);
            serviceRepo.Save(service);
            
            return RedirectToAction("Details", new { id = model.Id });
        }

        // GET: Service/Delete/5
        public ActionResult Delete(int id)
        {
            var model = GetModelWithId(id);
            
            return View(model);
        }

        // POST: Service/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ServiceOperationViewModel model)
        {
            var service = serviceRepo.ResultTable.Where(x => x.Id == model.Id).FirstOrDefault();
            serviceRepo.Delete(service);
            return RedirectToAction("Index");
        }

        private ServiceOperationViewModel GetModelWithId(int id)
        {
            var service = serviceRepo.ResultTable.Where(x => x.Id == id).FirstOrDefault();              
            var providerList = new List<SelectListItem>();
            var recipientList = new List<SelectListItem>();

            foreach (var provider in service.ServiceProviders)
            {
                var listItem = new SelectListItem();
                listItem.Text = provider.OrganizationName;
                listItem.Value = provider.Id.ToString();
                providerList.Add(listItem);              
            }

            foreach (var recipient in service.ServiceRecipients)
            {
                var listItem = new SelectListItem();
                listItem.Text = recipient.OrganizationName;
                listItem.Value = recipient.Id.ToString();
                recipientList.Add(listItem);            
            }

            var model = new ServiceOperationViewModel()
            {
                Id = service.Id,
                SelectedServiceTypeId = service.ServiceTypeId,
                ServiceName = service.ServiceName,
                ServiceDetails = service.ServiceDetails,
                Providers = providerList,
                Recipients = recipientList
            };

            return model;
        }

        private Service GetServiceFromViewModel (ServiceOperationViewModel model)
        {
            var service = new Service();
            service.Id = model.Id;
            service.ServiceName = model.ServiceName;
            service.ServiceDetails = model.ServiceDetails;

            service.ServiceTypeId = 1;
            service.ServiceType = serviceTypeRepo.ResultTable
                .Where(x => x.Id == model.SelectedServiceTypeId)
                .FirstOrDefault();
            service.ServiceProviders = providerRepo.ResultTable
                .Where(x => x.Id == model.SelectedProviderId)
                .ToList();
            service.ServiceRecipients = recipientRepo.ResultTable
                .Where(x => x.Id == model.SelectedRecipientId)
                .ToList();

            return service;
        }


        private ServiceOperationViewModel SaveServiceFromViewModel (ServiceOperationViewModel model)
        {
            var service = new Service();
            service.ServiceName = model.ServiceName;
            service.ServiceDetails = model.ServiceDetails;

            service.ServiceTypeId = model.SelectedServiceTypeId;
            service.ServiceType = _db.ServiceTypes.Find(model.SelectedServiceTypeId);
            service.ServiceProviders = _db.ServiceProviders
                .Where(x => x.Id == model.SelectedProviderId)
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
