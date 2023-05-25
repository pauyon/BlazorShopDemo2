using BlazorShopDemo2.ClientApp.Services.IService;
using BlazorShopDemo2.Domain.Models.Authentication;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorShopDemo2.ClientApp.Pages.Authentication
{
    public partial class Login
    {
        private MudForm _form { get; set; } = new();
        private SignInRequestDto _login = new();
        private bool _isLoading;
        private bool _showErrors;
        private string Errors;

        [Inject]
        public IAuthenticationService AuthService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private async Task Submit()
        {
            await _form.Validate();

            if (!_form.IsValid)
            {
                return;
            }

            _showErrors = false;
            _isLoading = true;
            var result = await AuthService.Login(_login);

            if (result.IsAuthSuccessfull)
            {
                NavigationManager.NavigateTo("/");
            }
            else
            {
                Errors = result.ErrorMessage;
                _showErrors = true;
            }

            _isLoading = false;
        }
    }
}