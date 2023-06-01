using BlazorShopDemo2.ClientApp.Services.IService;
using BlazorShopDemo2.ClientApp.ViewModels;
using BlazorShopDemo2.Domain.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorShopDemo2.ClientApp.Pages
{
    public partial class Details
    {
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        [Parameter]
        public int ProductId { get; set; }

        private DetailsViewModel _detailsViewModel = new();

        private ProductDto _product { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            IsLoading = true;
            _product = await ProductService.Get(ProductId);
            IsLoading = false;
        }

        private async Task AddToCart()
        {
            await MudForm!.Validate();

            if (MudForm.IsValid)
            {
                var shoppingCart = new ShoppingCart()
                {
                    ProductId = _product.Id,
                    Quantity = _detailsViewModel.Quantity,
                    ProductPriceId = _detailsViewModel.ProductPriceId
                };

                await ShoppingCartService.AddToCart(shoppingCart);

                SnackbarService.Add($"Item added to cart", Severity.Info);
            }
        }
    }
}