using asp.netcoreIdentityApp.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace asp.netcoreIdentityApp.Web.CustomValidations
{
    public class UserValidator : IUserValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            var errors = new List<IdentityError>();
            var isDigit = int.TryParse(user.UserName[0]!.ToString(), out _);

            if (isDigit)
            {
                errors.Add(new() { Code = " UserNameContainFirstLetterDigit", Description = "Kullnıcı adının ilk karakteri sayısal bir karakter olamaz." });
            }

            if (errors.Any())
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }

            return Task.FromResult(IdentityResult.Success);
        }
    }
}