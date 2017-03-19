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

        public ServiceController()
        {
            this.serviceRepo = new EFServiceRepository(_db);
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
            model.ServiceType = _db.ServiceTypes
                .Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() })
                .ToList();   
            model.Providers = _db.ServiceProviders
                .Select(x => new SelectListItem() { Text = x.OrganizationName, Value = x.Id.ToString() })
                .ToList();
            model.Recipients = _db.ServiceRecipients
                .Select(x => new SelectListItem() { Text = x.OrganizationName, Value = x.Id.ToString() })
                .ToList();

            return View(model);
        }

        // POST: Service/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ServiceOperationViewModel model)
        {
            var returnedModel = SaveServiceFromViewModel(model);
            if (returnedModel.Id != 0)
            {
                return RedirectToAction("Details", new { id = returnedModel.Id });
            }

            model.Providers = _db.ServiceProviders
               .Select(x => new SelectListItem() { Text = x.OrganizationName, Value = x.Id.ToString() })
               .ToList();
            model.Recipients = _db.ServiceRecipients
                .Select(x => new SelectListItem() { Text = x.OrganizationName, Value = x.Id.ToString() })
                .ToList();
            return View(model);
           
        }

        // GET: Service/Edit/5
        public ActionResult Edit(int id)
        {
            var service = new Service();
            service = _db.Services.Find(id);
            return View(service);
        }

        // POST: Service/Edit/5
        [HttpPost]
        public ActionResult Edit(Service model)
        {
            _db.Entry(model).CurrentValues.SetValues(model);
            _db.SaveChanges();
            return RedirectToAction("Details", new { id = model.Id });
        }

        // GET: Service/Delete/5
        public ActionResult Delete(int id)
        {
            var model = _db.Services.Find(id);
            return View(model);
        }

        // POST: Service/Delete/5
        [HttpPost]
        public ActionResult Delete(Service model)
        {
            _db.Services.Remove(model);
            _db.SaveChanges();
            return RedirectToAction("Details", new { id = model.Id });
        }

        private ServiceOperationViewModel GetModelWithId(int id)
        {
            var service = new Service();
            service = _db.Services.Find(id);
            var model = new ServiceOperationViewModel();

            model.Id = service.Id;
            model.ServiceName = service.ServiceName;
            model.ServiceDetails = service.ServiceDetails;
            var selectlist = new List<SelectListItem>();
            selectlist.Add(new SelectListItem() { Text = service.ServiceType.Name, Value = service.ServiceType.Id.ToString() }) ;
            model.ServiceType = selectlist;

            foreach (var provider in service.ServiceProviders)
            {
                var listItem = new SelectListItem();
                listItem.Text = provider.OrganizationName;
                listItem.Value = provider.Id.ToString();
                model.Providers.Add(listItem);
            }

            return model;
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
