using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Pharmacy.Web.Core.Models.Accounts;

namespace Pharmacy.Web.Core.DbInitializers
{
    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext> 
    {
        protected override void Seed(ApplicationDbContext context)
        {
            if (!context.Roles.Any(role => role.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole(roleName: "Admin");

                manager.Create(role);
            }

            if (!context.Users.Any(user => user.UserName == "Admin"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser()
                {
                    UserName = "Admin"
                };

                manager.Create(user, "boss00");
                manager.AddToRole(user.Id, "Admin");
            }

            if (!context.Roles.Any(role => role.Name == "Visitor"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole(roleName: "Visitor");

                manager.Create(role);
            }

            base.Seed(context);
        }
    }
}
