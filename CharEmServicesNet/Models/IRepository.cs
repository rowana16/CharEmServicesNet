using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CharEmServicesNet.Models
{
    public class IRepository
    {
        public interface IGenericRepository<T>
        {
            IQueryable<T> ResultTable { get; }
            T Save(T input);
            void Delete(T input);
        }

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

        public interface IUserRepository
        {
            IQueryable<ApplicationUser> ResultTable { get; }
            Task<ApplicationUser> Save(ApplicationUser user);            
            Task Delete(ApplicationUser service);
        }

        public interface ILocationRepository
        {
            IQueryable<Location> ResultTable { get; }
            Location Save(Location location);
            void Delete(Location location);
        }
    }
}