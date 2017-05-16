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