using CharEmServicesNet.Models;
using CharEmServicesNet.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace CharEmServicesNet.Controllers
{
    public class ServiceProviderController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: ServiceProvider
        public ActionResult Index()
        {
            var model = new IndexProviderViewModel();
            var names = new List<string>();
            var cities = new List<string>();

            var providers = _db.ServiceProviders.ToList();
            var addresses = _db.Addresses.ToList();
                        
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
            return View(GetModelWithProviderId(id));
        }

        // GET: ServiceProvider/Create
        public ActionResult Create()
        {
            var model = new CreateProviderViewModel();

            return View(model);
        }

        // POST: ServiceProvider/Create
        [HttpPost]
        public ActionResult Create(CreateProviderViewModel model)
        {
            var newProvider = new ServiceProvider();
            var newAddress = new Address();

            newAddress.Address1 = model.Address1;
            newAddress.Address2 = model.Address2;
            newAddress.City = model.City;
            newAddress.State = model.State;
            newAddress.Zip = model.Zip;

            _db.Addresses.Add(newAddress);
            _db.SaveChanges();

            newProvider.AddressId = newAddress.Id;
            newProvider.OrganizationName = model.OrganizationName;
            newProvider.Description = model.Description;
            newProvider.OrganizationTypeId = 5;
            newProvider.UserId = User.Identity.GetUserId();

            _db.ServiceProviders.Add(newProvider);
            _db.SaveChanges();

            return RedirectToAction("Edit",new { id = newProvider.Id });
        }

        // GET: ServiceProvider/Edit/5
        public ActionResult Edit(int id)
        {
            return View(GetModelWithProviderId(id));
            
        }

        // POST: ServiceProvider/Edit/5
        [HttpPost]
        public ActionResult Edit(CreateProviderViewModel model)
        {
            var currProvider = _db.ServiceProviders.Where(i => i.Id == model.ProviderId).First();
            var currAddress = _db.Addresses.Where(i => i.Id == currProvider.AddressId).First();

            currProvider.OrganizationName = model.OrganizationName;
            currProvider.Description = model.Description;
            currAddress.Address1 = model.Address1;
            currAddress.Address2 = model.Address2;
            currAddress.City = model.City;
            currAddress.State = model.State;
            currAddress.Zip = model.Zip;

            _db.Entry(currProvider).CurrentValues.SetValues(currProvider);
            _db.Entry(currAddress).CurrentValues.SetValues(currAddress);
            _db.SaveChanges();

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
            var delProvider = _db.ServiceProviders.Where(i => i.Id == model.ProviderId).First();
            var delAddress = _db.Addresses.Where(i => i.Id == delProvider.AddressId).First();
            _db.ServiceProviders.Remove(delProvider);
            _db.Addresses.Remove(delAddress);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        private CreateProviderViewModel GetModelWithProviderId (int providerId)
        {
            var currProvider = _db.ServiceProviders.Where(i => i.Id == providerId).First();
            var currAddress = _db.Addresses.Where(i => i.Id == currProvider.AddressId).First();

            var model = new CreateProviderViewModel();
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

            return model;
        }
    }
}
