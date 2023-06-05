using Blazored.LocalStorage;
using BlazorShopDemo2.ClientApp.Services.IService;
using BlazorShopDemo2.ClientApp.ViewModels;
using BlazorShopDemo2.Domain.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorShopDemo2.ClientApp.Pages.Cart
{
    public partial class Cart
    {
        [Inject]
        public ILocalStorageService LocalStorageService { get; set; }

        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        private List<ShoppingCart> _shoppingCart = new();
        private IEnumerable<ProductDto> _products = new List<ProductDto>();
        private decimal _orderTotal = 0.0m;

        protected override async Task OnInitializedAsync()
        {
            IsLoading = true;
            await LoadData();
            IsLoading = false;
        }

        private async Task LoadData()
        {
            _shoppingCart = await LocalStorageService.GetItemAsync<List<ShoppingCart>>(Common.Constants.ShoppingCart);
            _products = await ProductService.GetAll();

            foreach (var cart in _shoppingCart)
            {
                cart.Product = _products.FirstOrDefault(x => x.Id == cart.ProductId);
                cart.ProductPrice = cart.Product.ProductPrices.FirstOrDefault(x => x.Id == cart.ProductPriceId);
                _orderTotal += (cart.ProductPrice.Price * cart.Quantity);
            }
        }

        private async Task UpdateCartItem(ShoppingCart item)
        {
            if (item.Quantity <= 0)
            {
                await RemoveCartItem(item);
                return;
            }
            else
            {
                await ShoppingCartService!.UpdateCart(item);
                SnackbarService.Add("Item quantity updated", MudBlazor.Severity.Success);
                await LoadData();
            }
        }

        private async Task RemoveCartItem(ShoppingCart item)
        {
            await ShoppingCartService!.RemoveFromCart(item);
            SnackbarService.Add("Item removed from cart", MudBlazor.Severity.Success);
            await LoadData();
        }
    }
}