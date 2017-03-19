using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static CharEmServicesNet.Models.IRepository;

namespace CharEmServicesNet.Models
{
    public class EFServiceRepository : IServiceRepository
    {

        private ApplicationDbContext context;

        public EFServiceRepository(ApplicationDbContext _db)
        {
            context = _db;
        }

        public IQueryable<Service> ResultTable
        {
            get
            {
                return context.Services;
            }
        }

        public void Delete(Service service)
        {
            context.Services.Remove(service);
        }

        public Service Save(Service service)
        {
            if (service.Id == 0)
            {
                context.Services.Add(service);
                
            }
            else
            {
                context.Entry(service).State = System.Data.Entity.EntityState.Modified;
            }

            context.SaveChanges();
            return service;
        }
    }
}