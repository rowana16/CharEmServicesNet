using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static CharEmServicesNet.Models.IRepository;

namespace CharEmServicesNet.Models
{
    public class EFCountyRepository : IGenericRepository<County>
    {
         private ApplicationDbContext context;

        public EFCountyRepository(ApplicationDbContext _db)
        {
            context = _db;
        }

        public IQueryable<County> ResultTable
        {
            get
            {
                return context.Counties;
            }
        }

        public void Delete(County county)
        {
            context.Counties.Remove(county);
            context.SaveChanges();
        }

        public County Save(County county)
        {
            if(county.Id == 0)
            {
                context.Counties.Add(county);
            }
            else
            {
                context.Entry(county).State = System.Data.Entity.EntityState.Modified;
            }

            context.SaveChanges();
            return county;
        }
    }
}