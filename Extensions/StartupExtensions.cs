using asp.netcoreIdentityApp.Web.CustomValidations;
using asp.netcoreIdentityApp.Web.Localizations;
using asp.netcoreIdentityApp.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Web.Extensions
{
    public static class StartupExtensions
    {
        public static void AddIdentityWithExt(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnoprstuvywxz1234567890_";

                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;

                options.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromMinutes(3);
                options.Lockout.MaxFailedAccessAttempts =3;


            }).AddPasswordValidator<PasswordValidor>()
              .AddUserValidator<UserValidator>()
              .AddErrorDescriber<LocalizationIdentityErrorDescriber>()
              .AddEntityFrameworkStores<AppDbContext>();

        }
    }
}