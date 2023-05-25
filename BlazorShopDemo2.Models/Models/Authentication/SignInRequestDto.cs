using System.ComponentModel.DataAnnotations;

namespace BlazorShopDemo2.Domain.Models.Authentication
{
    public class SignInRequestDto
    {
        [Required(ErrorMessage = "Username is required")]
        [RegularExpression("^[a-zA-z0-9_.-]+@[]a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid Email Address")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}