using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalAO.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        /// <summary>
        /// Zarządca użytkowników
        /// </summary>
        [NotMapped]
        public virtual ApplicationUserManager UserManager { get; set; }

        /// <param name="userManager">Zarządca użytkowników</param>
        public ApplicationUser(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        /// <summary>
        /// Konstruktor bezparametrowt
        /// </summary>
        public ApplicationUser()
            : this(new ApplicationUserManager(new CustomUserStore(new ApplicationDbContext())))
        {
        }

        /// <summary>
        /// Opisuje imię użytkownika
        /// </summary>
        [Display(Name = "Imię")]
        public string Name { get; set; }

        /// <summary>
        /// Opisuje nazwisko użytkownika
        /// </summary>
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        /// <summary>
        /// Opisuje adres zamieszkania użytkownika
        /// </summary>
        [Display(Name = "Adres zamieszkania")]
        public string Address { get; set; }
        
        /// <summary>
        /// Opisuje numer telefonu użytkownika
        /// </summary>
        [Display(Name = "Numer telefonu")]
        public override string PhoneNumber { get; set; }

        /// <summary>
        /// Opisuje PESEL użytkownika
        /// </summary>
        [Display(Name = "PESEL")]
        public string PESEL { get; set; }

        /// <summary>
        /// Opisuje opinie o użytkowniku
        /// </summary>
        public virtual ICollection<RateModel> Rates { get; set; }

        /// <summary>
        /// Opisuje zlecenia użytkownika
        /// </summary>
        public virtual ICollection<AdvertisementModel> Advertisements { get; set; }

        /// <param name="manager"></param>
        /// <returns></returns>
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}