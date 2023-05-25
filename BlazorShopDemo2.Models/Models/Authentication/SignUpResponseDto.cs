namespace BlazorShopDemo2.Domain.Models.Authentication
{
    public class SignUpResponseDto
    {
        public bool IsRegistrationSuccessful { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}