using CharEmServicesNet.Models;
using CharEmServicesNet.Models.ViewModels;
using CharEmServicesNet.UserHelpers;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static CharEmServicesNet.Models.IRepository;

namespace CharEmServicesNet.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _db;
        private IUserRepository userRepo;
        private IGenericRepository<Location> locationRepo;
        private IGenericRepository<ServiceProvider> providerRepo;
        private IGenericRepository<City> cityRepo;
        private IGenericRepository<County> countyRepo;
        private UserRolesHelper roleHelper;

        public HomeController()
        {
            _db = new ApplicationDbContext();
            userRepo = new EFUserRepository(_db);
            locationRepo = new EFLocationRepository(_db);
            providerRepo = new EFProviderRepository(_db);
            roleHelper = new UserRolesHelper(_db);
            cityRepo = new EFCityRepository(_db);
            countyRepo = new EFCountyRepository(_db);
        }

        public ActionResult Index()
        {            
            ApplicationUser currentUser = new ApplicationUser();
            string currentRole = " ";
            var currentId = User.Identity.GetUserId();
           
            if (currentId != null)
            {
                currentUser = userRepo.ResultTable.First(x => x.Id == currentId);
                currentRole = roleHelper.ListUserRoles(currentId).First();
            }

            //var currentProvider = providerRepo.ResultTable.Where(x => x.UserId == currentUser.Id).FirstOrDefault().Id;
            bool IsAdmin = (currentRole == "UnitedWayAdmin");
            bool IsProvider = (currentRole == "ServiceProvider");
            
            List<Location> locations = locationRepo.ResultTable.ToList();
            List<City> cities = cityRepo.ResultTable.ToList();
            List<County> counties = countyRepo.ResultTable.ToList();

            var model = new MainViewModel(locations, cities, counties) {
                currentId = currentId,
                currentUser = currentUser,
                IsAdmin = IsAdmin,
                IsProvider = IsProvider
             };

            //if (currentProvider != null)
            //{
            //    model.ProviderId = currentProvider;
            //}
            

            
            return View(model);

        }

        public ActionResult LocationPartial(List<string> selectedLocations)
        {
            var services = new List<Service>();
            foreach(var selectedLocation in selectedLocations)
            {
                var locationId = Convert.ToInt32(selectedLocation);
                var location = locationRepo.ResultTable.Where(x => x.Id == locationId).FirstOrDefault();
                foreach(var service in location.Services)
                {
                    services.Add(service);
                }
            }

            var model = new LocationPartialViewModel(services);

            return PartialView(model);
           

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}