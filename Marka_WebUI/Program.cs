using Marka_BLL.Abstract;
using Marka_BLL.Concrete;
using Marka_DAL.Abstract;
using Marka_DAL.Concrete;
using Marka_DAL.Memory;
using Marka_WebUI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddScoped<IProductDal, ProductDal>();
builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<ICategoryDal, CategoryDal>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();

builder.Services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Latest);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.CustomStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
});
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name:"products",
        pattern:"products/{category?}",
        defaults: new {controller="Shop",action="List"}
        );
    endpoints.MapControllerRoute(
        name: "adminProducts",
        pattern: "admin/products",
        defaults: new { controller = "Admin", action = "ProductList" }
    );
    endpoints.MapControllerRoute(
        name: "adminProducts",
        pattern: "admin/products/{id?}",
        defaults: new { controller = "Admin", action = "EditProduct" }
);
});
SeedDatabase.Seed();
app.Run();

