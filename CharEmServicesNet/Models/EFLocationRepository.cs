using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static CharEmServicesNet.Models.IRepository;

namespace CharEmServicesNet.Models
{
    public class EFLocationRepository : IGenericRepository<Location>
    {
        private ApplicationDbContext context;

        public EFLocationRepository(ApplicationDbContext _db)
        {
            context = _db;
        }
        public IQueryable<Location> ResultTable
        {
            get
            {
                return context.Locations;
            }
        }

        public void Delete(Location location)
        {
            context.Locations.Remove(location);
            context.SaveChanges();
        }

        public Location Save(Location location)
        {
            if (location.Id == 0)
            {
                context.Locations.Add(location);
            }
            else
            {
                context.Entry(location).State = System.Data.Entity.EntityState.Modified;
            }

            context.SaveChanges();
            return location;
        }
    }
}