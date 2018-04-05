using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using PortalAO.Models;

namespace PortalAO
{ 
    /// <summary>
    /// Zarządza użytkownikami
    /// </summary>
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser, int>
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="store">Magazyn użytkowników</param>
        public ApplicationUserManager(IUserStore<ApplicationUser, int> store)
            : base(store)
        {
        }
        /// <summary>
        /// Metoda tworząca instancję klasy
        /// </summary>
        /// <param name="options">Opcje tworzenia Identity</param>
        /// <param name="context">Owin Context</param>
        /// <returns>Instancja klasy ApplicationUserManager</returns>
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new CustomUserStore(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new CustomValidator(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser, int>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser, int>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser, int>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    /// <summary>
    /// Zarządza logowania
    /// </summary>
    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, int>
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="userManager">Zarządca użytkownikami</param>
        /// <param name="authenticationManager">Zarządca uwierzytelniania</param>
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }
        /// <summary>
        /// Metoda zwracająca instancję klasy
        /// </summary>
        /// <param name="options">Opcje</param>
        /// <param name="context">Owin Context</param>
        /// <returns>Instancja klasy ApplicationSignInManager</returns>
        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }

    /// <summary>
    /// Zmodyfikowany walidator na potrzeby walidacji zaszyfrowanych pól użytkownika
    /// </summary>
    public class CustomValidator : UserValidator<ApplicationUser, int>
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="manager">Zarządza użytkowników</param>
        public CustomValidator(UserManager<ApplicationUser, int> manager) : base(manager)
        {
        }
        /// <summary>
        /// Walidacja użytkownika
        /// </summary>
        /// <param name="item">użytkownik do walidacji</param>
        /// <returns></returns>
        public override Task<IdentityResult> ValidateAsync(ApplicationUser item)
        {
            return base.ValidateAsync(item);
        }
    }
}
