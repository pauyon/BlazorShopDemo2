using Blazored.LocalStorage;
using BlazorShopDemo2.ClientApp.ViewModels;
using BlazorShopDemo2.Domain.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorShopDemo2.ClientApp.Pages.Cart
{
    public partial class Cart
    {
        [Inject]
        public ILocalStorageService LocalStorageService { get; set; }

        private List<ShoppingCart> _shoppingCart = new();
        private IEnumerable<ProductDto> _products = new List<ProductDto>();
        private decimal _orderTotal = 0.0m;

        protected override async Task OnInitializedAsync()
        {
            IsLoading = true;
            _shoppingCart = await LocalStorageService.GetItemAsync<List<ShoppingCart>>(Common.Constants.ShoppingCart);
            _products = await ProductService.GetAll();

            foreach(var cart in _shoppingCart)
            {
                cart.Product = _products.FirstOrDefault(x => x.Id == cart.ProductId);
                cart.ProductPrice = cart.Product.ProductPrices.FirstOrDefault(x => x.Id == cart.ProductPriceId);
                _orderTotal += (cart.ProductPrice.Price * cart.Quantity);
            }

            IsLoading = false;
        }
    }
}