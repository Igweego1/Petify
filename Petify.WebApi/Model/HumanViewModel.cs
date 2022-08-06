using System.ComponentModel.DataAnnotations;

namespace Petify.WebApi.Model
{
    public class HumanViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "First name is required")]
        [MaxLength(50, ErrorMessage = "Maximum length is exceeded")]
        [MinLength(2, ErrorMessage = "Minimum length is 2")]
        public string? FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name is required")]
        [MaxLength(50, ErrorMessage = "Maximum length is exceeded")]
        [MinLength(2, ErrorMessage = "Minimum length is 2")]
        public string? LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "User name is required")]
        [MaxLength(50, ErrorMessage = "Maximum length is exceeded")]
        [MinLength(2, ErrorMessage = "Minimum length is 2")]
        public string? UserName { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [MaxLength(50, ErrorMessage = "Maximum length is exceeded")]
        [MinLength(2, ErrorMessage = "Minimum length is 2")]
        public string? Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Confirm password is required")]
        [MaxLength(50, ErrorMessage = "Maximum length is exceeded")]
        [MinLength(2, ErrorMessage = "Minimum length is 2")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match")]
        public string? ConfirmPassword { get; set; }




        //public string Id { get; set; } = null!;
        //public int UserId { get; set; }
        //public string? FirstName { get; set; }
        //public string? LastName { get; set; }
        //public DateTime Created { get; set; }
        //public string? UserName { get; set; }
        //public string? NormalizedUserName { get; set; }
        //public string? Email { get; set; }
        //public string? NormalizedEmail { get; set; }
        //public bool EmailConfirmed { get; set; }
        //public string? PasswordHash { get; set; }
        //public string? SecurityStamp { get; set; }
        //public string? ConcurrencyStamp { get; set; }
        //public string? PhoneNumber { get; set; }
        //public bool PhoneNumberConfirmed { get; set; }
        //public bool TwoFactorEnabled { get; set; }
        //public DateTimeOffset? LockoutEnd { get; set; }
        //public bool LockoutEnabled { get; set; }
        //public int AccessFailedCount { get; set; }
    }
}
