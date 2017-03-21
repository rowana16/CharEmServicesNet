using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static CharEmServicesNet.Models.IRepository;

namespace CharEmServicesNet.Models
{
    public class EFServiceTypeRepository : IServiceTypeRepository
    {
        private ApplicationDbContext context;

        public EFServiceTypeRepository(ApplicationDbContext _db)
        {
            context = _db;
        }

        public IQueryable<ServiceType> ResultTable
        {
            get
            {
                return context.ServiceTypes;
            }
        }

        public void Delete(ServiceType serviceType)
        {
            context.ServiceTypes.Remove(serviceType);
        }

        public ServiceType Save(ServiceType serviceType)
        {
            if (serviceType.Id == 0)
            {
                context.ServiceTypes.Add(serviceType);
            }
            else
            {
                context.Entry(serviceType).State = System.Data.Entity.EntityState.Modified;
            }

            context.SaveChanges();
            return serviceType;
        }
    }
}