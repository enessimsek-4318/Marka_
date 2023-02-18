using Marka_BLL.Abstract;
using Marka_BLL.Concrete;
using Marka_DAL.Abstract;
using Marka_DAL.Concrete;
using Marka_DAL.Memory;
using Marka_WebUI.Identity;
using Marka_WebUI.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));


builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
    .AddDefaultTokenProviders();

var userManager=builder.Services.BuildServiceProvider().GetService<UserManager<ApplicationUser>>();
var roleManager = builder.Services.BuildServiceProvider().GetService<RoleManager<IdentityRole>>();
builder.Services.Configure<IdentityOptions>(options =>
{
    //password

    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = true;

    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.AllowedForNewUsers = true;

    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.SignIn.RequireConfirmedEmail = true;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/account/login";
    options.LogoutPath = "/account/logout";
    options.AccessDeniedPath = "/account/accessDenied";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60); 
    options.SlidingExpiration = true;
    options.Cookie = new CookieBuilder
    {
        HttpOnly = true,
        Name = "Marka.Security.Cookie",
        SameSite = SameSiteMode.Strict // Cookie sadece tarayýcýdan server tarafýna taþýnýr.
    };
});


builder.Services.AddScoped<IProductDal, ProductDal>();
builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<ICategoryDal, CategoryDal>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<ICommentDal, CommentDal>();
builder.Services.AddScoped<ICommentService, CommentManager>();
builder.Services.AddScoped<ICartDal, CartDal>();
builder.Services.AddScoped<ICartService, CartManager>();

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
app.UseAuthentication();
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
        name: "products",
        pattern: "products/{category?}",
        defaults: new { controller = "Shop", action = "List" });

    endpoints.MapControllerRoute(
        name: "adminProducts",
        pattern: "admin/products",
        defaults: new { controller = "Admin", action = "ProductList" });

    endpoints.MapControllerRoute(
        name: "adminProducts",
        pattern: "admin/products/{id?}",
        defaults: new { controller = "Admin", action = "EditProduct" });

    endpoints.MapControllerRoute(
       name: "adminCategories",
       pattern: "admin/categories/{id?}",
       defaults: new { controller = "Admin", action = "EditCategory" });
});
SeedDatabase.Seed();
SeedIdentity.Seed(userManager,roleManager, app.Configuration).Wait();
app.Run();

