using BlazorShopDemo2.ClientApp.Services.IService;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorShopDemo2.ClientApp.Pages
{
    public class PageBase : ComponentBase
    {
        [Inject]
        public IProductService ProductService { get; set; }

        [Inject]
        public ISnackbar SnackbarService { get; set; }

        public bool IsLoading { get; set; } = true;

        public MudForm? MudForm { get; set; } = new();
    }
}