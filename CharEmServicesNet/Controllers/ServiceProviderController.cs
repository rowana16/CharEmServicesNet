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
            var model = new IndexAdminProviderViewModel();
            
            = _db.ServiceProviders.ToList();

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
            return View();
        }

        // POST: ServiceProvider/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
