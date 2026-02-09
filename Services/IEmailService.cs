namespace asp.netcoreIdentityApp.Web.Services
{
    public interface IEmailService
    {
        Task SendResetPasswordEmail(string resetPaswordEmailLink,string ToEmail);
    }
}