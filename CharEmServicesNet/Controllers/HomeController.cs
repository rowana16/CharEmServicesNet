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
        private ILocationRepository locationRepo;
        private IServiceProviderRepository providerRepo;
        private UserRolesHelper roleHelper;

        public HomeController()
        {
            _db = new ApplicationDbContext();
            userRepo = new EFUserRepository(_db);
            locationRepo = new EFLocationRepository(_db);
            providerRepo = new EFProviderRepository(_db);
            roleHelper = new UserRolesHelper(_db);
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
            bool IsAdmin = (currentRole == "UnitedWayAdmin");
            List<Location> locations = locationRepo.ResultTable.ToList();
            var model = new MainViewModel(locations) {
                currentId = currentId,
                currentUser = currentUser,
                IsAdmin = IsAdmin
            };
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
            
          

            return PartialView(services);
           

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