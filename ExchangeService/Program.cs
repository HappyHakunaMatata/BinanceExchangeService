using ExchangeService.Implementations.Interfaces;
using ExchangeService.Implementations.Realisations;
using Microsoft.AspNetCore.Hosting;
using ExchangeService.DataBase;
using Microsoft.EntityFrameworkCore;
using ExchangeService.DataBase.Interfaces;
using ExchangeService.DataBase.Repository;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAccount, AccountImplementation>();
builder.Services.AddScoped<IKline, KlineImplementation>();
builder.Services.AddScoped<IOrder, OrderImplementation>();
builder.Services.AddScoped<IExchangeInfo, ExchangeInfoImplementation>();
builder.Services.AddTransient<IHistoryAsset, HistoryAssetRepository>();
builder.Services.AddTransient<IFee, FeeImplementation>();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddMvc();

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

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=DashBoard}/{action=Index}/{id?}");

app.Run();

