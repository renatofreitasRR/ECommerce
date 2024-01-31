using ECommerce.Api.Customers.DataBase;
using ECommerce.Api.Customers.Interfaces;
using ECommerce.Api.Customers.Profiles;
using ECommerce.Api.Customers.Providers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder
    .Services
    .AddScoped<ICustomersProvider, CustomersProvider>();

builder
    .Services
    .AddAutoMapper(typeof(CustomerProfiler));

builder
    .Services
    .AddDbContext<CustomersDbContext>(options =>
    {
        options.UseInMemoryDatabase("Customers");
    });

builder
    .Services
    .AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapControllers();

app.Run();
