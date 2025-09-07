using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;
using MvcRoutingApp.Constraints;
using MvcRoutingApp;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

// Top-level route registrations
app.MapControllerRoute(
    name: "productDetails",
    pattern: "Products/{category}/{id:int}",
    defaults: new { controller = "Products", action = "Details" });

app.MapControllerRoute(
    name: "userOrders",
    pattern: "Users/{username}/Orders",
    defaults: new { controller = "Users", action = "Orders" });

app.MapControllerRoute(
    name: "dashboard",
    pattern: "Dashboard/{role}",
    defaults: new { controller = "Dashboard", action = "Index" });

app.MapControllerRoute(
    name: "guidRoute",
    pattern: "Products/Guid/{id:guid}",
    defaults: new { controller = "Products", action = "ByGuid" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();

namespace MvcRoutingApp
{
    public partial class Program { }
}
