using System.ComponentModel.DataAnnotations;

namespace BlazorShopDemo2.Domain.Models
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public bool ShopFavorites { get; set; }
        public bool CustomerFavorites { get; set; }
        public string Color { get; set; }
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public CategoryDto Category { get; set; }
    }
}