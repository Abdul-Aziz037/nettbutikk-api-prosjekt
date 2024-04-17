using Microsoft.EntityFrameworkCore;
using nettbutikk_api.Data;
using nettbutikk_api.Repositories.Interfaces;
using nettbutikk_api.Repositories;
using nettbutikk_api.Mappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

//register mappers
builder.Services.AddAutoMapper(typeof(ProductMapper));

// registrerer repos
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// registrerer services



// registere DbMysql
builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 21))));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
