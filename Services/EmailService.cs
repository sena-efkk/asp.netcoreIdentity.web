
using System.Net;
using System.Net.Mail;
using asp.netcoreIdentityApp.Web.OptionsModel;
using Microsoft.Extensions.Options;

namespace asp.netcoreIdentityApp.Web.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailsetting;
        public EmailService(IOptions<EmailSettings> options)
        {
            this._emailsetting=options.Value;
        }

        public async Task SendResetPasswordEmail(string resetPaswordEmailLink, string ToEmail)
        {
            var smtp =new SmtpClient();

            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Port = 587 ;
            smtp.Credentials = new NetworkCredential(_emailsetting.Email,_emailsetting.Password);
            smtp.EnableSsl = true;
            smtp.Host = _emailsetting.Host;

            var mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(_emailsetting.Email);
            mailMessage.To.Add(ToEmail);
            mailMessage.Subject = "LocalHost | Şifre sıfırlama linki";
            mailMessage.Body = @$"
            <h4>Şİfrenizi yenilemek için aşağıdaki linkte tıklayınız</h4>
            <p><a href='{resetPaswordEmailLink}'>Şifre yenileme link</a></p>";


            mailMessage.IsBodyHtml = true;

            await smtp.SendMailAsync(mailMessage);
        }
    }
}