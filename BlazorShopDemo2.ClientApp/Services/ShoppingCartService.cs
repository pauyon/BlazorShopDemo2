using Blazored.LocalStorage;
using BlazorShopDemo2.ClientApp.Services.IService;
using BlazorShopDemo2.ClientApp.ViewModels;
using static MudBlazor.CategoryTypes;

namespace BlazorShopDemo2.ClientApp.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly ILocalStorageService _localStorageService;

        public ShoppingCartService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task AddToCart(ShoppingCart item)
        {
            var cart = await _localStorageService.GetItemAsync<List<ShoppingCart>>(Common.Constants.ShoppingCart);

            if (cart == null) cart = new List<ShoppingCart>();

            var cartItem = cart.FirstOrDefault(x => x.ProductId == item.ProductId && x.ProductPriceId == item.ProductPriceId);

            if (cartItem != null)
            {
                cart[cart.IndexOf(cartItem)].Quantity += item.Quantity;
            }
            else
            {
                cart.Add(item);
            }

            await _localStorageService.SetItemAsync(Common.Constants.ShoppingCart, cart);
        }

        public async Task RemoveFromCart(ShoppingCart item)
        {
            var cart = await _localStorageService.GetItemAsync<List<ShoppingCart>>(Common.Constants.ShoppingCart);

            var cartItem = cart.FirstOrDefault(x => x.ProductId == item.ProductId && x.ProductPriceId == item.ProductPriceId);

            if (cartItem != null)
            {
                if (cartItem.Quantity == 0 || cartItem.Quantity == 1)
                {
                    cart.Remove(cartItem);
                }
                else
                {
                    cart[cart.IndexOf(cartItem)].Quantity -= cartItem.Quantity;
                }
            }

            await _localStorageService.SetItemAsync(Common.Constants.ShoppingCart, cart);
        }
    }
}