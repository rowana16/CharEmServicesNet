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
            this.store = new UserStore<ApplicationUser>(_db);
            this.userManager = new UserManager<ApplicationUser>(store);
            this.roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_db));

        }


        public IQueryable<ApplicationUser> ResultTable
        {
            get
            {
                return context.Users;
            }
        }

        public async Task Delete(ApplicationUser user)
        {
            await store.DeleteAsync(user);
            context.SaveChanges();
        }

        public async Task<ApplicationUser> Save(ApplicationUser user)
        {
            if(user.Id == null)
            {
                await store.CreateAsync(user);
            }
            else
            {
                await store.UpdateAsync(user);
            }
            context.SaveChanges();
            return user;
        }

        public bool IsUserInRole(string userId, string roleName)
        {
            return userManager.IsInRole(userId, roleName);
        }

        public IList<string> ListUserRoles(string userId)
        {
            return userManager.GetRoles(userId);
        }

        public IList<string> ListAbsentUserRoles(string userId)
        {
            List<string> currentUserRoles = ListUserRoles(userId).ToList();
            IList<IdentityRole> allRoleItems = roleManager.Roles.ToList();
            List<string> absentRoles = new List<string>();
            bool found = false;

            foreach (IdentityRole role in allRoleItems)
            {
                foreach (string currentRole in currentUserRoles)
                {
                    if (role.Name == currentRole) { found = true; }
                }

                if (!found) { absentRoles.Add(role.Name); }
                found = false;
            }
            return (absentRoles);
        }

        public bool AddUserToRole(string userId, string roleName)
        {
            var result = userManager.AddToRole(userId, roleName);
            return result.Succeeded;
        }

        public bool RemoveUserFromRole(string userId, string roleName)
        {
            var result = userManager.RemoveFromRole(userId, roleName);
            return result.Succeeded;
        }

        public IList<ApplicationUser> UsersInRole(string roleName)
        {
            var userIds = roleManager.FindByName(roleName).Users.Select(r => r.UserId);
            return userManager.Users.Where(u => userIds.Contains(u.Id)).ToList();
        }

    }
}