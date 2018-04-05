using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using PortalAO.Models;

[assembly: OwinStartupAttribute(typeof(PortalAO.Startup))]
namespace PortalAO
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();
        }

        /// <summary>
        /// Metoda tworząca role w bazie.
        /// </summary>
        private void CreateRolesAndUsers()
        {
            ApplicationDbContext dbContext = ApplicationDbContext.Create();

            var roleManager = new RoleManager<CustomRole, int>(new CustomRoleStore(dbContext));
            var userManager = new UserManager<ApplicationUser, int>(new CustomUserStore(dbContext));
            userManager.UserValidator = new CustomValidator(userManager) { AllowOnlyAlphanumericUserNames = false };

            // In Startup iam creating first Admin Role and creating a default Admin User 
            if (!roleManager.RoleExists("Administrator"))
            {
                var role = new CustomRole();
                role.Name = "Administrator";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.Name = "Janusz";
                user.Surname = "Admin";
                user.Address = "tajny";
                user.PESEL = "xxx";
                user.PhoneNumber = "999999999";
                user.UserName = "admin@admin.com";
                user.Email = "admin@admin.com";

                string userPWD = "Qwerty_123!";

                var chkUser = userManager.Create(user, userPWD);

                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "Administrator");
                }
            }

            if (!roleManager.RoleExists("User"))
            {
                var role = new CustomRole();
                role.Name = "User";
                roleManager.Create(role);
            }

            dbContext.SaveChanges();
        }
    }
}
