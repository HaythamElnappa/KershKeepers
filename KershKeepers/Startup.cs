using System;
using System.Security.Policy;
using KershKeepers.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KershKeepers.Startup))]
namespace KershKeepers
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            roleManager.Create(new IdentityRole() { Name = "Admin" });
            //DateTime date = DateTime.Today;
            ApplicationDbContext db = new ApplicationDbContext();
            var user = db.Users.Find("96a10cf3-10df-497e-bef8-d6003ffc613d");
            UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            manager.AddToRole("1f49b932-0d7d-49a0-aa17-91c38a1ba8e8", "Admin");
        }
    }
}
