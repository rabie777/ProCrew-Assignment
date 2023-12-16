using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ProCrew_Assignment.DTO
{
    
    public class ProductDto
    {

        [Required(ErrorMessage = "Product name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Product description is required.")]
        public string Description { get; set; }

        [Range(1, 1000000, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        [Range(1, 1000, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }
    }
}
