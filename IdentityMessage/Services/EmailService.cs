
using IdentityMessage.ViewModels;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace IdentityMessage.Services;

public class EmailService : IEmailService
{
    private readonly EmailSettings  _options;

    public EmailService(IOptions<EmailSettings> options)
    {
        _options = options.Value;
    }

    public async Task SendResetEmail(string resetEmailLink, string To)
    {
        var smtpClient = new SmtpClient();
        
        var mailMessage = new MailMessage();

        smtpClient.Host = _options.Host;
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Port = 587;
        smtpClient.Credentials = new NetworkCredential(_options.Email, _options.Password);
        smtpClient.EnableSsl = true;

        mailMessage.From = new MailAddress(_options.Email);
        mailMessage.To.Add(To);

        mailMessage.Subject = "Şifre Sıfırlama Linki";

        mailMessage.Body = @$"
        <h4>Şifrenizi yenilemek için aşağıdaki linke tıklayınız.</h4>
        <p><a href='{resetEmailLink}'>Şifre yenilemek için lütfen tıklayınız.   </a></p>";

        mailMessage.IsBodyHtml = true;

        await smtpClient.SendMailAsync(mailMessage);
    }
}
