using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static CharEmServicesNet.Models.IRepository;

namespace CharEmServicesNet.Models
{
    public class EFRecipientRepository : IServiceRecipientRepository
    {
        private ApplicationDbContext context;

        public EFRecipientRepository(ApplicationDbContext _db)
        {
            context = _db;
        }
        public IQueryable<ServiceRecipient> ResultTable
        {
            get
            {
                return context.ServiceRecipients;
            }
        }

        public void Delete(ServiceRecipient serviceRecipient)
        {
            context.ServiceRecipients.Remove(serviceRecipient);
        }

        public ServiceRecipient Save(ServiceRecipient serviceRecipient)
        {
            if (serviceRecipient.Id == 0)
            {
                context.ServiceRecipients.Add(serviceRecipient);

            }
            else
            {
                context.Entry(serviceRecipient).State = System.Data.Entity.EntityState.Modified;
            }

            context.SaveChanges();
            return serviceRecipient;
        }
    }
}