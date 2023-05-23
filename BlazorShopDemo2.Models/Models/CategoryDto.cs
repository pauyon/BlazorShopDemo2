using System.ComponentModel.DataAnnotations;

namespace BlazorShopDemo2.Domain.Models
{
    public class CategoryDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}