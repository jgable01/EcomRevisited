using EcomRevisited.Data;
using EcomRevisited.Services;
using EcomRevisited.Services.EcomRevisited.Services; 
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure Services
builder.Services.AddDbContext<EcomDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EcomConnectionString")));

// Register repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Register the services
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<CountryService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<ProductService>();

// Add services for MVC and Razor Pages
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Add session services
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); 
});

// Build the app
var app = builder.Build();

// Configure Middleware
app.UseStaticFiles();

// Use Session middleware
app.UseSession();  

app.UseRouting();

app.UseAuthorization();

// Map controllers and Razor pages
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Catalogue}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
