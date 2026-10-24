using Domain.Enums;

namespace Application.interfaces.ExternalServices;

public interface IEmailService
{
    string GenerateEmailBody(string token, string email, string firstname, Gender? gender);
    string GenerateEmailSubject();
    Task SendEmailAsync(string to, string subject, string body);
}