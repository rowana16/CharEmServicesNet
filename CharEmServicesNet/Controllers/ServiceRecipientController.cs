﻿using CharEmServicesNet.Models;
using CharEmServicesNet.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CharEmServicesNet.Controllers
{
    public class ServiceRecipientController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: ServiceRecipient
        public ActionResult Index()
        {
            var model = new IndexRecipientViewModel();
            var names = new List<string>();
            var cities = new List<string>();

            var recipients = _db.ServiceProviders.ToList();
            var addresses = _db.Addresses.ToList();

            foreach (var recipient in recipients)
            {
                var currItem = new IndexRecipientItem();
                currItem.RecipientId = recipient.Id;
                currItem.OrganizationName = recipient.OrganizationName;
                var currAddress = addresses.Where(a => a.Id == recipient.AddressId);
                currItem.City = currAddress.First().City;
                model.ServiceRecipients.Add(currItem);
            }

            return View(model);
        }

        // GET: ServiceRecipient/Details/5
        public ActionResult Details(int id)
        {
            return View(GetModelWithRecipientId(id));
        }

        // GET: ServiceRecipient/Create
        public ActionResult Create()
        {
            var model = new CreateRecipientViewModel();

            return View(model);
        }

        // POST: ServiceRecipient/Create
        [HttpPost]
        public ActionResult Create(CreateRecipientViewModel model)
        {
            var newRecipient = new ServiceProvider();
            var newAddress = new Address();

            newAddress.Address1 = model.Address1;
            newAddress.Address2 = model.Address2;
            newAddress.City = model.City;
            newAddress.State = model.State;
            newAddress.Zip = model.Zip;

            _db.Addresses.Add(newAddress);
            _db.SaveChanges();

            newRecipient.AddressId = newAddress.Id;
            newRecipient.OrganizationName = model.OrganizationName;
            newRecipient.Description = model.Description;
            newRecipient.OrganizationTypeId = 5;
            newRecipient.UserId = User.Identity.GetUserId();

            _db.ServiceProviders.Add(newRecipient);
            _db.SaveChanges();

            return RedirectToAction("Edit", new { id = newRecipient.Id });
        }

        // GET: ServiceRecipient/Edit/5
        public ActionResult Edit(int id)
        {
            return View(GetModelWithRecipientId(id));
        }

        // POST: ServiceRecipient/Edit/5
        [HttpPost]
        public ActionResult Edit(CreateRecipientViewModel model)
        {
            var currRecipient = _db.ServiceProviders.Where(i => i.Id == model.RecipientId).First();
            var currAddress = _db.Addresses.Where(i => i.Id == currRecipient.AddressId).First();

            currRecipient.OrganizationName = model.OrganizationName;
            currRecipient.Description = model.Description;
            currAddress.Address1 = model.Address1;
            currAddress.Address2 = model.Address2;
            currAddress.City = model.City;
            currAddress.State = model.State;
            currAddress.Zip = model.Zip;

            _db.Entry(currRecipient).CurrentValues.SetValues(currRecipient);
            _db.Entry(currAddress).CurrentValues.SetValues(currAddress);
            _db.SaveChanges();

            return RedirectToAction("Details", new { id = model.RecipientId });
        }

        // GET: ServiceRecipient/Delete/5
        public ActionResult Delete(int id)
        {
            return View(GetModelWithRecipientId(id));
        }

        // POST: ServiceRecipient/Delete/5
        [HttpPost]
        public ActionResult Delete(CreateRecipientViewModel model)
        {
            var delRecipient = _db.ServiceProviders.Where(i => i.Id == model.RecipientId).First();
            var delAddress = _db.Addresses.Where(i => i.Id == delRecipient.AddressId).First();
            _db.ServiceProviders.Remove(delRecipient);
            _db.Addresses.Remove(delAddress);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        private CreateRecipientViewModel GetModelWithRecipientId(int recipientId)
        {
            var currRecipient = _db.ServiceProviders.Where(i => i.Id == recipientId).First();
            var currAddress = _db.Addresses.Where(i => i.Id == currRecipient.AddressId).First();

            var model = new CreateRecipientViewModel();
            model.Address1 = currAddress.Address1;
            model.Address2 = currAddress.Address2;
            model.City = currAddress.City;
            model.State = currAddress.State;
            model.Zip = currAddress.Zip;
            model.OrganizationName = currRecipient.OrganizationName;
            model.Description = currRecipient.Description;
            model.AddressId = currAddress.Id;
            model.OrganizationTypeId = currRecipient.OrganizationTypeId;
            model.UserId = currRecipient.UserId;
            model.RecipientId = recipientId;

            return model;
        }
    }
}