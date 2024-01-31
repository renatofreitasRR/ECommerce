using ECommerce.Api.Products.DataBase;
using ECommerce.Api.Products.Interfaces;
using ECommerce.Api.Products.Profiles;
using ECommerce.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder
    .Services
    .AddScoped<IProductsProvider, ProductsProvider>();

builder
    .Services
    .AddAutoMapper(typeof(ProductProfile));

builder
    .Services
    .AddDbContext<ProductsDbContext>(options =>
    {
        options.UseInMemoryDatabase("Products");
    });

builder
    .Services
    .AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapControllers();

app.Run();
