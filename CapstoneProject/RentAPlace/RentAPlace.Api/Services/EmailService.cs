// Services/EmailService.cs
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

public class SmtpSettings
{
    public string Host { get; set; } = "";
    public int Port { get; set; } = 587;
    public string User { get; set; } = "";
    public string Pass { get; set; } = "";
    public bool UseSsl { get; set; } = true;
    public string FromName { get; set; } = "RentAPlace";
    public string FromEmail { get; set; } = "no-reply@rentaplace.test";
}

public class EmailService : IEmailService
{
    private readonly SmtpSettings _settings;
    public EmailService(IOptions<SmtpSettings> options)
    {
        _settings = options.Value;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string bodyHtml)
    {
        var msg = new MimeMessage();
        msg.From.Add(new MailboxAddress(_settings.FromName, _settings.FromEmail));
        msg.To.Add(new MailboxAddress(toEmail, toEmail));
        msg.Subject = subject;

        var builder = new BodyBuilder { HtmlBody = bodyHtml };
        msg.Body = builder.ToMessageBody();

        using var client = new SmtpClient();
        var secure = _settings.UseSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto;
        await client.ConnectAsync(_settings.Host, _settings.Port, secure);
        if (!string.IsNullOrWhiteSpace(_settings.User))
            await client.AuthenticateAsync(_settings.User, _settings.Pass);

        await client.SendAsync(msg);
        await client.DisconnectAsync(true);
    }
}
