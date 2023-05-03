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

        [HttpGet]
        [Route("GetProducts")]
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

        [HttpGet]
        [Route("UserExists")]
        public async Task<User> UserExists([FromQuery] string userName)
        {
            var client = await _dbContext.Users.FirstOrDefaultAsync(prop => prop.Name == userName);

            if (client == null)
            {
                return new User();
            }
            else
            {
                return client;
            }
        }

        [HttpGet]
        [Route("GetUsers")]
        public async Task<User[]> GetUsers([FromQuery] int pagination = 10)
        {
            return await _dbContext.Users
            .OrderBy(p => p.Points)
            .Take(pagination)
            .ToArrayAsync();
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (!_dbContext.Users.Any(p => p.Name == user.Name))
            {
                var newUser = new User
                {
                    Name = user.Name,
                    Points = user.Points
                };

                await _dbContext.Users.AddAsync(newUser);
                _dbContext.SaveChanges();
            }
            

            return StatusCode(200);
        }

        [HttpPost]
        [Route("UpdateUserPoints")]
        public async Task<IActionResult> UpdateUserPoints([FromBody] User user)
        {
            var person = _dbContext.Users.FirstOrDefault(p => p.Id == user.Id);

            if (person != null)
            {
                person.Points = user.Points;
                await _dbContext.SaveChangesAsync();

            }

            return StatusCode(200);
        }

        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] Items item)
        {
            var newProduct = new Product
            {
                Name = item.Name,
                Price = item.Price,
                Description = item.Description,
                Points = item.Points
            };

            await _dbContext.Products.AddAsync(newProduct);
            _dbContext.SaveChanges();

            return StatusCode(200);
        }


    }
}
