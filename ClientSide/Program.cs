using DataLayer.Context;
using DinkToPdf.Contracts;
using DinkToPdf;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Services;
using ServiceLayer.Services.Interfaces;
using Rotativa.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

}).AddCookie(options =>
{
    options.LoginPath = "/Login";
    options.LogoutPath = "/SignOut";
    options.ExpireTimeSpan = TimeSpan.FromDays(15);
});


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 5;
});
builder.Services.Configure<SecurityStampValidatorOptions>(x =>
{
    x.ValidationInterval = TimeSpan.Zero;
});

builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));



builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")));

builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IAdminService, AdminService>();


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
app.UseAuthentication();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: "adminpanel",
    pattern: "{area:exists}/{controller=Account}/{action=Index}/{id?}"
    );
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{controller=Home}/{action=Index}/{id?}"
    );

});

app.Run();
