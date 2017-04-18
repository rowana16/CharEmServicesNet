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
        public ActionResult Details(int id)
        {
            return View();
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

        public ActionResult Update(string Id)
        {
            var model = new UpdateUserViewModel(userRepo.ResultTable.Where(x => x.Id == Id).First());
            return View(model);
        }

        [HttpPost]
        public ActionResult Update(UpdateUserViewModel model)
        {
            model.UpdateUser.UserName = model.UpdateUser.Email;
            model.UpdateUser.DisplayName = model.UpdateUser.FirstName + " " + model.UpdateUser.LastName;

            var result = userRepo.Save(model.UpdateUser);
            return RedirectToAction("index");
        }

        public ActionResult Delete(string Id)
        {
            var user = userRepo.ResultTable.Where(x => x.Id == Id).First();
            var model = new DeleteUserViewModel(user);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(DeleteUserViewModel model)
        {
            userRepo.Delete(model.DeleteUser);
            _db.SaveChanges();
            return RedirectToAction("index");
        }

       
    }
}
