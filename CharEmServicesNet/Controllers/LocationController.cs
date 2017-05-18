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
        private ApplicationDbContext _currentDb;

        public LocationController(ApplicationDbContext _db)
        {
            locationRepo = new EFLocationRepository(_db);
            _currentDb = _db;
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
            var model = new LocationCreateViewModel(_currentDb);
            return View(model);
        }

        // POST: Location/Create
        [HttpPost]
        public ActionResult Create(LocationCreateViewModel model)
        {
            var selectedCity = Convert.ToInt16(model.SelectedCity);
            var selectedCounty = Convert.ToInt16(model.SelectedCounty);

            var newLocation = new Location()
            {
                LocationName = model.LocationName,
                LocationDescription = model.LocationDescription,
                City = _currentDb.Cities.Where(x => x.Id == selectedCity).FirstOrDefault(),
                County = _currentDb.Counties.Where(x=> x.Id == selectedCounty).FirstOrDefault()
            };

            try
            {
               locationRepo.Save(newLocation);
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
            var model = new LocationEditViewModel(_currentDb,id);
            return View(model);
        }

        // POST: Location/Edit/5
        [HttpPost]
        public ActionResult Edit(LocationEditViewModel model)
        {
            var currentLocation = locationRepo.ResultTable.Where(x => x.Id == model.Id).FirstOrDefault();
            var selectedCity = currentLocation.CityId;
            if (model.SelectedCity != "") { selectedCity =  Convert.ToInt16(model.SelectedCity); }
            var selectedCounty = currentLocation.CountyId;
            if (model.SelectedCounty != "") { selectedCounty = Convert.ToInt16(model.SelectedCounty); }
            

            if(currentLocation.CityId != selectedCity)
            {
                currentLocation.City = _currentDb.Cities.Where(x => x.Id == selectedCity).FirstOrDefault();

            }

            if(currentLocation.CountyId != selectedCounty)
            {
                currentLocation.County = _currentDb.Counties.Where(x => x.Id == selectedCounty).FirstOrDefault();
            }
                                    
            if (model.LocationName != null)
            {
                currentLocation.LocationName = model.LocationName;
            }

            if (model.LocationDescription != null)
            {
                currentLocation.LocationDescription = model.LocationDescription;
            }              
            

            try
            {
                var location = locationRepo.Save(currentLocation);
               
                return RedirectToAction("details",new { id = location.Id });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View();
            }
            
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
