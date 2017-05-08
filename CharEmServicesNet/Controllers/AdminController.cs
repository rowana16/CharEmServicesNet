using CharEmServicesNet.Models;
using CharEmServicesNet.Models.ViewModels;
using CharEmServicesNet.UserHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static CharEmServicesNet.Models.IRepository;

namespace CharEmServicesNet.Controllers
{
   // [Authorize(Roles = "UnitedWayAdmin")]
    public class AdminController : Controller
    {

        ApplicationDbContext _db = new ApplicationDbContext();
        IUserRepository userRepo;

        public AdminController()
        {
            this.userRepo = new EFUserRepository(_db);
        }
        

        // GET: Admin
        public ActionResult Index()
        {
            var model = new AdminIndexViewModel();
            model.Users = userRepo.ResultTable.ToList();
            return View(model);
        }

        // GET: Admin/Details/5
        public ActionResult Details(string id)
        {
            var currUser = userRepo.ResultTable.Where(x => x.Id == id).First();

            var model = new UpdateUserViewModel() {
                Id = id,
                FirstName = currUser.FirstName,
                LastName = currUser.LastName,
                Email = currUser.Email,
                Phone = currUser.PhoneNumber,
                CurrentRole = currUser.Roles.First().ToString(),
                
            };
            
            return View(model);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
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

        // Update - Get
        [Authorize(Roles = "UnitedWayAdmin")]
        public ActionResult Update(string Id)
        {
            var currUser = userRepo.ResultTable.Where(x => x.Id == Id).First();
            var helper = new UserRolesHelper(_db);
            var model = new UpdateUserViewModel()
            {
                Id = Id,
                FirstName = currUser.FirstName,
                LastName = currUser.LastName,
                Email = currUser.Email,
                Phone = currUser.PhoneNumber                
            };

            try
            {
                model.CurrentRole = helper.ListUserRoles(Id).First();
            }
            catch
            {
                model.CurrentRole =  "No Current Role" ;
            }
            var selectListSetup = helper.ListAbsentUserRoles(Id).ToList();
            selectListSetup.Insert(0, " ");

            model.AvailableRoles = new SelectList(selectListSetup);
            
            return View(model);
        }

        // Update - Post
        [HttpPost]
        [Authorize(Roles = "UnitedWayAdmin")]
        public ActionResult Update(UpdateUserViewModel model)
        {
            var helper = new UserRolesHelper(_db);
            var currUser = userRepo.ResultTable.Where(x => x.Id == model.Id).First();

            currUser.FirstName = model.FirstName;
            currUser.LastName = model.LastName;
            currUser.DisplayName = model.FirstName + " " + model.LastName;
            currUser.Email = model.Email;
            currUser.PhoneNumber = model.Phone;
            currUser.UserName = model.Email;
            if(model.SelectedRole != " " && model.SelectedRole != null)
            {
                helper.AddUserToRole(model.Id, model.SelectedRole);
                helper.RemoveUserFromRole(model.Id, model.CurrentRole);
            }
            
            var result = userRepo.Save(currUser);
            return RedirectToAction("index");
        }

        public ActionResult Delete(string Id)
        {
            var user = userRepo.ResultTable.Where(x => x.Id == Id).First();
            var model = new DeleteUserViewModel(user);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(ApplicationUser user)
        {
            var delUser = userRepo.ResultTable.Where(x => x.Id == user.Id).First();

            userRepo.Delete(delUser);
            //_db.SaveChanges();
            return RedirectToAction("index");
        }

       
    }
}
