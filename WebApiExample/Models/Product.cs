using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApiExample.Models
{
    public class Product
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public double Points { get; set; }
    }

    public static class ProductsSeedData
    {
        public static readonly List<Product> Products = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "Americano",
                Price = 7.99,
                Description = "Black coffee with water",
                Points = 0.1
            },
            new Product
            {
                Id = 2,
                Name = "Expresso",
                Price = 6.99,
                Description = "Coffee",
                Points = 0.1
            },
            new Product
            {
                Id = 3,
                Name = "Latte",
                Price = 2.99,
                Description = "Milk with coffee",
                Points = 0.1
            }
        };
    }
}
