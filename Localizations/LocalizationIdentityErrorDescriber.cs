using Microsoft.AspNetCore.Identity;

namespace asp.netcoreIdentityApp.Web.Localizations
{
    public class LocalizationIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string userName)
        {
            return new() { Code = "DublicateUserName" , Description = $"Bu {userName} başkasına ait"};
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new() { Code = "DublicateEmail" , Description = $"Bu {email} başkasına ait"};
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new() { Code = "PasswordTooShort" , Description = $"Şifre en az 6 karakter olmalı"};
        }


    }
}