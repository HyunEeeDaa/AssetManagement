using AssetManagement.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionSting = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connction string  not found");
//if (builder.Configuration.GetConnectionString("DefaultConnection") == null)
//{
//    throw new InvalidOperationException("Connction string  not found");
//}
//else
//{
//    var connectionstring = builder.Configuration.GetConnectionString("DefaultConnection");
//}

// Add services to the container.
builder.Services.AddDbContext<asset_managementContext>(options => options.UseSqlServer(connectionSting));
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options => { options.IdleTimeout = TimeSpan.FromMinutes(60); options.Cookie.HttpOnly = true; options.Cookie.IsEssential = true; });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
