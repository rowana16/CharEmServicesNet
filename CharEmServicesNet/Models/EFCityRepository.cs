using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static CharEmServicesNet.Models.IRepository;

namespace CharEmServicesNet.Models
{
    public class EFCityRepository : IGenericRepository<City>
    {
        private ApplicationDbContext context;

        public EFCityRepository(ApplicationDbContext _db)
        {
            context = _db;
        }

        public IQueryable<City> ResultTable
        {
            get
            {
                return context.Cities;
            }
        }

        public void Delete(City city)
        {
            context.Cities.Remove(city);
            context.SaveChanges();
        }

        public City Save(City city)
        {
            if (city.Id == 0)
            {
                context.Cities.Add(city);
            }
            else
            {
                context.Entry(city).State = System.Data.Entity.EntityState.Modified;
            }

            context.SaveChanges();
            return city;
        }
    }
}