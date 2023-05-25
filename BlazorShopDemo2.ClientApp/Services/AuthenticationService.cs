using Blazored.LocalStorage;
using BlazorShopDemo2.ClientApp.Services.IService;
using BlazorShopDemo2.Domain.Models.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace BlazorShopDemo2.ClientApp.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStoreage;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthenticationService(HttpClient httpClient,
            ILocalStorageService localStoreage,
            AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _authStateProvider = authenticationStateProvider;
            _localStoreage = localStoreage;
        }

        public async Task<SignInResponseDto> Login(SignInRequestDto signInRequest)
        {
            var content = JsonConvert.SerializeObject(signInRequest);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/account/signin", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SignInResponseDto>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                await _localStoreage.SetItemAsync(Common.Constants.LocalToken, result.Token);
                await _localStoreage.SetItemAsync(Common.Constants.LocalUserDetails, result.User);

                ((AuthStateProvider)_authStateProvider).NotifyUserLoggedIn(result.Token);

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);

                return new SignInResponseDto() { IsAuthSuccessfull = true };
            }
            else
            {
                return result;
            }
        }

        public async Task Logout()
        {
            await _localStoreage.RemoveItemAsync(Common.Constants.LocalToken);
            await _localStoreage.RemoveItemAsync(Common.Constants.LocalUserDetails);

            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();

            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<SignUpResponseDto> RegisterUser(SignUpRequestDto signUpRequest)
        {
            var content = JsonConvert.SerializeObject(signUpRequest);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/account/signup", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SignUpResponseDto>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                return new SignUpResponseDto() { IsRegistrationSuccessful = true };
            }
            else
            {
                return new SignUpResponseDto { IsRegistrationSuccessful = false, Errors = result.Errors };
            }
        }
    }
}