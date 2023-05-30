using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorShopDemo2.Domain.Entities
{
    public class ProductPrice
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public string Size { get; set; }
        public double Price { get; set; }
    }
}