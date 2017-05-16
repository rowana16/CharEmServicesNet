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
    public class LocationController : Controller
    {
        private IGenericRepository<Location> locationRepo;

        public LocationController(ApplicationDbContext _db)
        {
            locationRepo = new EFLocationRepository(_db);
        }

        // GET: Location
        public ActionResult Index()
        {
            var locationSet = locationRepo.ResultTable.ToList();
            var model = new LocationIndexViewModel(locationSet);
            return View(model);
        }

        // GET: Location/Details/5
        public ActionResult Details(int id)
        {
            var location = locationRepo.ResultTable.Where(x => x.Id == id).First();
            var model = new LocationOperationViewModel(location);
            return View(model);
        }

        // GET: Location/Create
        public ActionResult Create()
        {           
            var model = new LocationCreateViewModel();
            return View(model);
        }

        // POST: Location/Create
        [HttpPost]
        public ActionResult Create(LocationEditViewModel model)
        {
            var newLocation = new Location()
            {                         
                LocationName = model.LocationName,
                LocationDescription = model.LocationDescription

            };

            try
            {
                model = new LocationEditViewModel(locationRepo.Save(newLocation));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View();
            }
            return RedirectToAction("index");
        }

        // GET: Location/Edit/5
        public ActionResult Edit(int id)
        {
            var location = locationRepo.ResultTable.Where(x => x.Id == id).First();
            var model = new LocationOperationViewModel(location);
            return View(model);
        }

        // POST: Location/Edit/5
        [HttpPost]
        public ActionResult Edit(LocationOperationViewModel model)
        {
            var newLocation = new Location()
            {
                Id = model.Id,
                LocationName = model.LocationName,
                LocationDescription = model.LocationDescription
            };

            try
            {
                model = new LocationOperationViewModel(locationRepo.Save(newLocation));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View();
            }
            return RedirectToAction("index");
        }

        // GET: Location/Delete/5
        public ActionResult Delete(int id)
        {
            var location = locationRepo.ResultTable.Where(x => x.Id == id).First();
            var model = new LocationOperationViewModel(location);
            return View(model);
        }

        // POST: Location/Delete/5
        [HttpPost]
        public ActionResult Delete(LocationOperationViewModel model)
        {
            var deleteLocation = locationRepo.ResultTable.Where(x => x.Id == model.Id).First();

            try
            {
                locationRepo.Delete(deleteLocation);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View();
            }
            return RedirectToAction("index");
        }
    }
}
