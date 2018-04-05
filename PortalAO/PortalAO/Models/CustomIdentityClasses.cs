using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Threading.Tasks;

namespace PortalAO.Models
{
    /// <summary>
    /// Zmodyfikowana klasa odpowiadająca za łączenie użytkowników z rolami
    /// </summary>
    public class CustomUserRole : IdentityUserRole<int> { }
    /// <summary>
    /// Zmodyfikowana klasa odpowiadająca za łączenie użytkowników z claim
    /// </summary>
    public class CustomUserClaim : IdentityUserClaim<int> { }
    /// <summary>
    /// Zmodyfikowana klasa odpowiadająca za logowanie
    /// </summary>
    public class CustomUserLogin : IdentityUserLogin<int> { }

    /// <summary>
    /// Typ wyliczeniowy dla ról
    /// </summary>
    public enum AppRoles
    {
        Administrator, Mandatory, Principal
    }

    /// <summary>
    /// Zmodyfikowana klasa odpowiadająca za role
    /// </summary>
    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public CustomRole() { }
        public CustomRole(string name) { Name = name; }
    }

    /// <summary>
    /// Zmodyfikowana klasa odpowiadająca za przechowywanie użytkowników
    /// </summary>
    public class CustomUserStore : UserStore<ApplicationUser, CustomRole, int,
        CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">kontekst bazy</param>
        public CustomUserStore(ApplicationDbContext context)
            : base(context)
        {
        }
        /// <summary>
        /// Przeszukiwanie użytkoników po email
        /// </summary>
        /// <param name="email">Adres email</param>
        /// <returns>Instancja użytkownika opakowaną klasą Task</returns>
        public override Task<ApplicationUser> FindByEmailAsync(string email)
        {
            var user = Users.ToList().FirstOrDefault(u => u.Email == email);
            return Task.FromResult(user);
        }
    }

    /// <summary>
    /// Zmodyfikowana klasa odpowiadająca za przechowywanie ról
    /// </summary>
    public class CustomRoleStore : RoleStore<CustomRole, int, CustomUserRole>
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Kontekst bazy</param>
        public CustomRoleStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
