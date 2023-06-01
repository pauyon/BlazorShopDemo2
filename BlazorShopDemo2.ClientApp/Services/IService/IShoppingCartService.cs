using BlazorShopDemo2.ClientApp.ViewModels;

namespace BlazorShopDemo2.ClientApp.Services.IService
{
    public interface IShoppingCartService
    {
        Task RemoveFromCart(ShoppingCart shoppingCart);

        Task AddToCart(ShoppingCart shoppingCart);
    }
}