using BlazorShopDemo2.ClientApp.Services.IService;
using BlazorShopDemo2.Domain.Models.Authentication;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorShopDemo2.ClientApp.Pages.Authentication
{
    public partial class Register
    {
        private MudForm _form { get; set; } = new();
        private SignUpRequestDto _register = new();
        private bool _isLoading;
        private bool _showErrors;
        private IEnumerable<string> Errors = new List<string>();

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
            var result = await AuthService.RegisterUser(_register);

            if (result.IsRegistrationSuccessful)
            {
                NavigationManager.NavigateTo("/login");
            }
            else
            {
                Errors = result.Errors;
                _showErrors = true;
            }

            _isLoading = false;
        }
    }
}