using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApiExample.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();


        var connection = "Server=localhost;Port=5432;Database=Shop;User Id=postgres;Password=admin;";
        // Add PostgreSQL database context to services
        builder.Services.AddDbContext<WebApiExample.Models.MyDbContext>(options =>
            options.UseNpgsql(connection));

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.WithOrigins("http://localhost:4200")
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
        });


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            // Seed the database with data
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();
                SeedData(dbContext);
            }

            app.UseCors();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    private static void SeedData(MyDbContext dbContext)
    {
        if (!dbContext.Products.Any())
        {
            dbContext.Products.AddRange(ProductsSeedData.Products);
            dbContext.SaveChanges();
        }

        if (!dbContext.Users.Any())
        {
            dbContext.Users.AddRange(UsersSeedData.Users);
            dbContext.SaveChanges();
        }
    }
}