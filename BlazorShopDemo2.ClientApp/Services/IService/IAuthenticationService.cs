using BlazorShopDemo2.Domain.Models.Authentication;

namespace BlazorShopDemo2.ClientApp.Services.IService
{
    public interface IAuthenticationService
    {
        Task<SignUpResponseDto> RegisterUser(SignUpRequestDto signUpRequest);

        Task<SignInResponseDto> Login(SignInRequestDto signInRequest);

        Task Logout();
    }
}