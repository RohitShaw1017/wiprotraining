// Services/IEmailService.cs
using System.Threading.Tasks;

public interface IEmailService
{
    Task SendEmailAsync(string toEmail, string subject, string bodyHtml);
}
