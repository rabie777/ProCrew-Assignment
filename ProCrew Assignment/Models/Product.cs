using Microsoft.EntityFrameworkCore;

namespace ProCrew_Assignment.Models
{
    [Index (nameof(Name),nameof(Price),nameof(Quantity),IsUnique =true)]
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
