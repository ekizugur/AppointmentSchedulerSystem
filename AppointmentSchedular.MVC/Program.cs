using AppointmentSchedular.Data.Context;
using AppointmentSchedular.Data.Extensions;
using AppointmentSchedular.Entity.Entities;
using AppointmentSchedular.Service.Extensions;
using FluentAssertions.Common;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.LoadServiceLayerExtension(builder.Configuration);
builder.Services.AddSession();
builder.Services.AddControllersWithViews();
builder.Services.LoadDataLayerExtension(builder.Configuration);
builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{ //identity yapýlanmasý
    opt.SignIn.RequireConfirmedAccount = true;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
})  .AddRoleManager<RoleManager<AppRole>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(config => {
    config.LoginPath = new PathString("/Auth/Login");
    config.LogoutPath = new PathString("/Auth/Logout");
    config.Cookie = new CookieBuilder
    {
        Name = "AppointmentSchedular",
        HttpOnly = true,
        SameSite = SameSiteMode.Strict,
        SecurePolicy = CookieSecurePolicy.Always
    };
    config.SlidingExpiration = true;
    config.ExpireTimeSpan=TimeSpan.FromDays(7);
    config.AccessDeniedPath = new PathString("/Auth/AccessDenied");

});



//identity yapýlanmasý son
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
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();
app.MapHangfireDashboard();
app.UseHangfireDashboard();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
