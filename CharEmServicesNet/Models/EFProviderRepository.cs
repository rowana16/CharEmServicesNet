using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static CharEmServicesNet.Models.IRepository;

namespace CharEmServicesNet.Models
{
    public class EFProviderRepository : IGenericRepository<ServiceProvider>
    {
        private ApplicationDbContext context;

        public EFProviderRepository(ApplicationDbContext _db)
        {
            context = _db;
        }

        public IQueryable<ServiceProvider> ResultTable
        {
            get
            {
                return context.ServiceProviders;
            }
        }

        public void Delete(ServiceProvider serviceProvider)
        {
            context.ServiceProviders.Remove(serviceProvider);
            context.SaveChanges();
        }

        public ServiceProvider Save(ServiceProvider serviceProvider)
        {
            if (serviceProvider.Id == 0)
            {
                context.ServiceProviders.Add(serviceProvider);

            }
            else
            {
                context.Entry(serviceProvider).State = System.Data.Entity.EntityState.Modified;
            }

            context.SaveChanges();
            return serviceProvider;
        }
    }
}