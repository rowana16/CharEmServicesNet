using CharEmServicesNet.Models;
using CharEmServicesNet.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            return View();
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
            newAddress.Zip = model.State;

            _db.Addresses.Add(newAddress);
            _db.SaveChanges();

            newProvider.AddressId = newAddress.Id;
            newProvider.OrganizationName = model.OrganizationName;
            newProvider.Description = model.Description;

            _db.ServiceProviders.Add(newProvider);
            _db.SaveChanges();

            return RedirectToAction("Edit", model);
        }

        // GET: ServiceProvider/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ServiceProvider/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ServiceProvider/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ServiceProvider/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
