using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static CharEmServicesNet.Models.IRepository;

namespace CharEmServicesNet.Models
{
    public class EFUserRepository : IUserRepository
    {
        private ApplicationDbContext context;

        public EFUserRepository(ApplicationDbContext _db)
        {
            context = _db;
        }


        public IQueryable<ApplicationUser> ResultTable
        {
            get
            {
                return context.Users;
            }
        }

        public void Delete(ApplicationUser user)
        {
            context.Users.Remove(user);
            context.SaveChanges();
        }

        public ApplicationUser Save(ApplicationUser user)
        {
            if (user.Id == null)
            {
                context.Users.Add(user);                
            }

            else
            {
                context.Entry(user).State = System.Data.Entity.EntityState.Modified;
            }

            context.SaveChanges();
            return user;
        }
    }
}