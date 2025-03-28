using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EvaluationASPNET.Data;
using EvaluationASPNET.Models;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<EvaluationASPNETContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EvaluationASPNETContext") ?? throw new InvalidOperationException("Connection string 'EvaluationASPNETContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(1);
    options.IOTimeout = TimeSpan.FromDays(1);
    options.Cookie.Name = ".MySampleMVCWEB.Session";
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.Path = "/";
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

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
    pattern: "{controller=Tickets}/{action=Index}/{id?}");

app.Run();
