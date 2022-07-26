using System.ComponentModel.DataAnnotations;

namespace ApiTask.DTOs
{
    public class AppUserToAddDto
    {
        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "3 to 15 characters allowed")]
        public string LastName { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "3 to 15 characters allowed")]
        public string FirstName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email{ get; set; }

        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "3 to 15 characters allowed")]

        public string Role { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match")]
        public string ConfirmPassword { get; set; }
        
    }
}
