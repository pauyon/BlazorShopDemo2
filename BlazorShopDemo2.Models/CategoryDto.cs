using System.ComponentModel.DataAnnotations;

namespace BlazorShopDemo2.Models
{
    public class CategoryDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}