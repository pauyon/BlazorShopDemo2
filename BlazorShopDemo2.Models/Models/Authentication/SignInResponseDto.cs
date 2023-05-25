namespace BlazorShopDemo2.Domain.Models.Authentication
{
    public class SignInResponseDto
    {
        public bool IsAuthSuccessfull { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
        public UserDto User { get; set; }
    }
}