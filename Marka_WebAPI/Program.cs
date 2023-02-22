using Marka_BLL.Abstract;
using Marka_BLL.Concrete;
using Marka_DAL.Abstract;
using Marka_DAL.Concrete;
using Marka_WebAPI.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("UserContext")));


builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IProductDal, ProductDal>();
builder.Services.AddScoped<IProductService, ProductManager>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
