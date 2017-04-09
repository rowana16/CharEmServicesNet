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
            IList<string> currentRole = new List<string>();
            var currentId = User.Identity.GetUserId();
            if (currentId != null)
            {
                currentUser = userRepo.ResultTable.First(x => x.Id == currentId);
                currentRole = roleHelper.ListUserRoles(currentId);
            }
           
            List<Location> locations = locationRepo.ResultTable.ToList();
            var model = new MainViewModel(locations) { currentId = currentId, currentUser = currentUser, currentRoles = currentRole};
            return View(model);
        }

        public ActionResult LocationPartial(string selectedLocation)
        {
            var locationId = Convert.ToInt32(selectedLocation);
            try
            {
                
                var providers = providerRepo.ResultTable
                    .Where(x => x.Locations
                        .Select(y=>y.Id).Contains(locationId))
                    .ToList();
                var model = new LocationPartialViewModel(providers);

                return PartialView(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
    }
}