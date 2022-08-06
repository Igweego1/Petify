using System.ComponentModel.DataAnnotations;

namespace Petify.Consume.Models
{
    public class RegisterViewModel
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


    }
}
