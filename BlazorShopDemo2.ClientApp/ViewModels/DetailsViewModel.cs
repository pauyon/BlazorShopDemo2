using BlazorShopDemo2.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace BlazorShopDemo2.ClientApp.ViewModels
{
    public class DetailsViewModel
    {
        public DetailsViewModel()
        {
            ProductPrice = new();
            Quantity = 1;
        }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value greater than 0")]
        public int Quantity { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select prodcut price")]
        public int ProductPriceId { get; set; }

        public ProductPriceDto ProductPrice { get; set; }
    }
}