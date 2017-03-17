using CharEmServicesNet.Models;
using CharEmServicesNet.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CharEmServicesNet.Controllers
{
    public class ServiceController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Service
        public ActionResult Index()
        {
            var model = new ServiceIndexViewModel();

            model.services = _db.Services.ToList();
            return View(model);
        }

        // GET: Service/Details/5
        public ActionResult Details(int id)
        {
            var service = new Service();
            service = _db.Services.Find(id);
            return View(service);
        }

        // GET: Service/Create
        public ActionResult Create()
        {
            var service = new Service();
            return View(service);
        }

        // POST: Service/Create
        [HttpPost]
        public ActionResult Create(Service model)
        {
            _db.Services.Add(model);
            return RedirectToAction("Details", new { id = model.Id });
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
    }
}
