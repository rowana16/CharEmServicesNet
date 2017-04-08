using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using static CharEmServicesNet.Models.IRepository;

namespace CharEmServicesNet.Models
{
    public class EFUserRepository : IUserRepository
    {
        private ApplicationDbContext context;
        private UserStore<ApplicationUser> store;
        private UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        public EFUserRepository(ApplicationDbContext _db)
        {
            this.context = _db;
            this.store = new UserStore<ApplicationUser>(context);
            //this.userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));            
            //this.roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));           
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

        public ApplicationUser Save(ApplicationUser user) { return new ApplicationUser(); }

        public async Task<ApplicationUser> SaveAsync(ApplicationUser user)
        {
            if (user.Id == null)
            {
                context.Users.Add(user);                
            }

            else
            {
               Task savedUser = store.UpdateAsync(user);
               await savedUser;
            }

            try
            {
                       
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return user;
        }
    }
}