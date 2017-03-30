using CharEmServicesNet.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CharEmServicesNet.Helpers
{
    public class UserRolesHelper
    {
        private ApplicationDbContext db;
        private UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        public UserRolesHelper(ApplicationDbContext context)
        {
            this.userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            this.roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));
            this.db = context;
        }

        public bool IsUserInRole (string userId, string roleName)
        {
            return userManager.IsInRole(userId, roleName);
        }

        public IList<string> ListCurrentRoles(string userId)
        {
            return userManager.GetRoles(userId);
        }

        public IList<string> ListAbsentRoles(string userId)
        {
            IList<string> currentUserRoles = ListCurrentRoles(userId).ToList();
            IList<IdentityRole> allRoleItems = roleManager.Roles.ToList();
            IList<string> absentRoles = allRoleItems.Select(x => x.Name).Except(currentUserRoles).ToList();

            return absentRoles;
        }

        public bool AddUserToRole(string userId, string roleName)
        {
            var result = userManager.AddToRole(userId, roleName);
            return result.Succeeded;
        }

        public bool RemoveUserFromRole(string userId, string roleName)
        {
            var result = userManager.RemoveFromRole( userId,  roleName);
            return result.Succeeded;            
        }

        public IList<ApplicationUser> AllUsersInRole(string roleName)
        {
            var userIds = roleManager.FindByName(roleName).Users.Select(r => r.UserId);
            return userManager.Users.Where(u => userIds.Contains(u.Id)).ToList();
        }

        public IList<string> GetAllRoles()
        {
            var roles =  db.Roles.Select(x=>x.Name).ToList();
            return roles;
        }       

    }
}