using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Abstract;
using StoreApp.Data.Concrete;
using StoreApp.Web.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(ProfileMapper).Assembly);
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); 
    options.Cookie.HttpOnly = true; 
    options.Cookie.IsEssential = true; 
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<Cart>(sc => SessionCart.GetCart(sc));

builder.Services.AddDbContext<StoreDbContext>(options => {
    options.UseSqlite(builder.Configuration["ConnectionStrings:StoreDbConnection"], b =>b.MigrationsAssembly("StoreApp.Web"));
});

builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();
builder.Services.AddScoped<IOrderRepository,EOrderRepository>();

var app = builder.Build();

app.UseStaticFiles();
app.UseSession();

app.MapControllerRoute("product_in_categories","products/{category?}",new {controller="Home", action="Index"});
app.MapControllerRoute("product_details","{name}",new {controller="Home", action="Details"});
app.MapRazorPages();

app.MapDefaultControllerRoute();


app.Run();
