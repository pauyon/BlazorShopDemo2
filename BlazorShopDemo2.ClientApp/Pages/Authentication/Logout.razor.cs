using BlazorShopDemo2.ClientApp.Services.IService;
using Microsoft.AspNetCore.Components;

namespace BlazorShopDemo2.ClientApp.Pages.Authentication
{
    public partial class Logout
    {
        [Inject]
        public IAuthenticationService AuthService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await AuthService.Logout();
            NavigationManager.NavigateTo("/");
        }
    }
}