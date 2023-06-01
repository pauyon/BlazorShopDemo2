using BlazorShopDemo2.Domain.Models;

namespace BlazorShopDemo2.ClientApp.ViewModels
{
    public class ShoppingCart
    {
        public int ProductId { get; set; }
        public ProductDto Product { get; set; }
        public int ProductPriceId { get; set; }
        public ProductPriceDto ProductPrice { get; set; }
        public int Quantity { get; set; }
    }
}