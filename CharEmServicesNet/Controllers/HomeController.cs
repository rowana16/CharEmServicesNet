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
        private EFServiceTypeRepository serviceTypeRepo;
        private EFServiceRepository serviceRepo;

        public HomeController()
        {
            _db = new ApplicationDbContext();
            userRepo = new EFUserRepository(_db);
            locationRepo = new EFLocationRepository(_db);
            providerRepo = new EFProviderRepository(_db);
            roleHelper = new UserRolesHelper(_db);
            cityRepo = new EFCityRepository(_db);
            countyRepo = new EFCountyRepository(_db);
            serviceTypeRepo = new EFServiceTypeRepository(_db);
            serviceRepo = new EFServiceRepository(_db);
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
            List<ServiceType> serviceTypes = serviceTypeRepo.ResultTable.ToList();
            

            var model = new MainViewModel(serviceTypes) {
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

        [HttpPost]
        public ActionResult LocationPartial(int id)
        {
            var services = serviceRepo.ResultTable.Where(s => s.ServiceTypeId == id);
            var locations = services
                .SelectMany(s => s.Locations)
                .Where(s => s.IsSchool == true)
                .Distinct().ToList();

            var model = new LocationPartialViewModel(locations);
            return PartialView(model);
           

        }

        [HttpPost]
        public ActionResult ServicePartial(int locationid, int servicetypeid)
        {
            var location = locationRepo.ResultTable.Where(l => l.Id == locationid).FirstOrDefault();
            try
            {
                var services = serviceRepo.ResultTable
                    .Where(s => s.ServiceTypeId == servicetypeid && s.Locations.Contains(locationRepo.ResultTable.Where(l => l.Id == locationid).FirstOrDefault()))
                    .ToList();
                var model = new ServicePartialViewModel(services);
                return PartialView(model);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }

            return View();
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

        public List<Service> GetServices(Location location)
        {
            var returnServices = new List<Service>();
            foreach(var service in location.Services)
            {
                returnServices.Add(service);
            }
            return returnServices;
        }
    }
}