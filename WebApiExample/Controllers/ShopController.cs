using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiExample.Dto;
using WebApiExample.Models;


namespace WebApiExample.Controllers
{
    [ApiController]
    [Route("Shop")]
    public class ShopController : ControllerBase
    {
        private MyDbContext _dbContext;

        public ShopController(MyDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        [Route("GetProducts")]
        [HttpGet]
        public async Task<ProductDto> GetProducts([FromQuery]int pagination = 10)
        {
            var data = await _dbContext.Products
            .OrderByDescending(p => p.Price)
            .Take(pagination)
            .ToArrayAsync();

            var output = new ProductDto()
            {
                products = new List<Items>()
            };
            foreach (var product in data)
            {
                output.products.Add(
                    new Items()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        Price = product.Price,
                        Points = product.Points
                    });
            }

            return output;
        }
    }
}
