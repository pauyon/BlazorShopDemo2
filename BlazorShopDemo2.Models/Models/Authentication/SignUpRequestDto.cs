using System.ComponentModel.DataAnnotations;

namespace BlazorShopDemo2.Domain.Models.Authentication
{
    public class SignUpRequestDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression("^[a-zA-z0-9_.-]+@[]a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirm password do not match")]
        public string ConfirmPassword { get; set; }
    }
}