using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CharEmServicesNet.Models
{
    public class IRepository
    {
        public interface IServiceRepository
        {
            IQueryable <Service> ResultTable { get; }
            Service Save(Service service);
            void Delete(Service service);
        }

        public interface IServiceProviderRepository
        {
            IQueryable<ServiceProvider> ResultTable { get; }
            ServiceProvider Save(ServiceProvider service);
            void Delete(ServiceProvider service);
        }

        public interface IServiceRecipientRepository
        {
            IQueryable<ServiceRecipient> ResultTable { get; }
            ServiceRecipient Save(ServiceRecipient service);
            void Delete(ServiceRecipient service);
        }

        public interface IAddressRepository
        {
            IQueryable<Address> ResultTable { get; }
            Address Save(Address service);
            void Delete(Address service);
        }

        public interface IServiceTypeRepository
        {
            IQueryable<ServiceType> ResultTable { get; }
            ServiceType Save(ServiceType service);
            void Delete(ServiceType service);
        }
    }
}