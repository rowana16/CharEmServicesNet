using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static CharEmServicesNet.Models.IRepository;

namespace CharEmServicesNet.Models
{
    public class EFAddressRepository : IGenericRepository<Address>
    {
        private ApplicationDbContext context;

        public EFAddressRepository(ApplicationDbContext _db)
        {
            context = _db;
        }

        public IQueryable<Address> ResultTable
        {
            get
            {
                return context.Addresses;
            }
        }

        public void Delete(Address address)
        {
            context.Addresses.Remove(address);
            context.SaveChanges();
        }

        public Address Save(Address address)
        {
            if(address.Id == 0)
            {
                context.Addresses.Add(address);
            }
            else
            {
                context.Entry(address).State = System.Data.Entity.EntityState.Modified;
            }

            context.SaveChanges();
            return address;
        }
    }
}