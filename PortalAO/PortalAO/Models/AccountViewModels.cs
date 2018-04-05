using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PortalAO.Models
{
    public class AccountEditViewModel
    {
        [Display(Name = "Imię")]
        public string Name { get; set; }

        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        [Display(Name = "Adres zamiekszania")]
        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telefon kontaktowy")]
        public string PhoneNumber { get; set; }

        [Display(Name = "PESEL")]
        public string PESEL { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Imię")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Adres zamieszkania")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Telefon kontaktowy")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "PESEL")]
        public string PESEL { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Zleceniodawca")]
        public bool Principal { get; set; }

        [Display(Name = "Zleceniobiorca")]
        public bool Mandatory { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Hasło musi mieć przynajmniej {2} znakóW", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Powtórz hasło")]
        [Compare("Password", ErrorMessage = "Hasło nie są takie same!")]
        public string ConfirmPassword { get; set; }
    }
}
