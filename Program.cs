using asp.netcoreIdentityApp.Web.Models;
using asp.netcoreIdentityApp.Web.OptionsModel;
using asp.netcoreIdentityApp.Web.Services;
using AspNetCoreIdentityApp.Web.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


var connectionString = builder.Configuration.GetConnectionString("ConnectionString");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddIdentityWithExt();
builder.Services.AddScoped<IEmailService,EmailService>();

builder.Services.ConfigureApplicationCookie(opt =>
{
    var cookiebuilder = new CookieBuilder();

    cookiebuilder.Name = "UdemyAppCookie";
    opt.LoginPath = new PathString("/Home/SignIn");
    opt.LogoutPath =new PathString("/Member/Logout");
    opt.Cookie = cookiebuilder;
    opt.ExpireTimeSpan = TimeSpan.FromDays(60);
    opt.SlidingExpiration = true;



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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();