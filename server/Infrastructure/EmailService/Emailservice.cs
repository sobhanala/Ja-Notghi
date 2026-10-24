using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Text;
using Application.interfaces.ExternalServices;
using Domain.Enums;

namespace Infrastructure.EmailService;

public class SmtpEmailService : IEmailService
{
    private readonly SmtpSettings _smtpSettings;

    public SmtpEmailService(IOptions<SmtpSettings> smtpSettings)
    {
        _smtpSettings = smtpSettings.Value;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        using (var smtpClient = new SmtpClient(_smtpSettings.Server, _smtpSettings.Port))
        {
            smtpClient.Credentials = new NetworkCredential(_smtpSettings.FromEmail, _smtpSettings.Password);
            smtpClient.EnableSsl = true;

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpSettings.FromEmail!, _smtpSettings.FromName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            mailMessage.To.Add(new MailAddress(to));

            await smtpClient.SendMailAsync(mailMessage);
        }
    }

    private string GenerateCallerVerb(Gender? gender)
    {
        switch (gender)
        {
            case Gender.Male:
                return "مشتی";
            case Gender.Female:
                return "خوشگله";
            case Gender.NonBinary:
                return "اوبی";
            case Gender.NotImportant:
                return "کون گشاد";
            default:
                return "ریدوم";
        }
    }

    public string GenerateEmailBody(string token, string email, string firstname, Gender? gender)
    {
        var htmlFilePath = _smtpSettings.ForgetPasswordHtmlPath!;
        var htmlTemplate = File.ReadAllText(htmlFilePath, Encoding.UTF8);
        
        var baseUrl = _smtpSettings.BaseUrl;
        var resetPasswordPath = _smtpSettings.ResetPasswordPath;
        var resetPasswordUrl = $"{baseUrl}{resetPasswordPath}?token={token}?email={email}";
        
        htmlTemplate = htmlTemplate.Replace("{{RESET_LINK}}", resetPasswordUrl);
        
        var janotghiLogoPath = "https://amiralirahimii.github.io/Assets/assets/Janotghi-text-logo.png";
        var uteamLogoPath = "https://amiralirahimii.github.io/Assets/assets/Uteam-text-logo.png";
        var eyeImagePath = "https://amiralirahimii.github.io/Assets/assets/eye-img.png";
        var copyrightLogoPath = "https://amiralirahimii.github.io/Assets/assets/copyright-logo.png";

        htmlTemplate = htmlTemplate.Replace("{0}", firstname);
        htmlTemplate = htmlTemplate.Replace("{1}", GenerateCallerVerb(gender));
        htmlTemplate = htmlTemplate.Replace("{{Janotghi-text-logo.svg}}", janotghiLogoPath);
        htmlTemplate = htmlTemplate.Replace("{{Uteam-text-logo.svg}}", uteamLogoPath);
        htmlTemplate = htmlTemplate.Replace("{{eye-img.svg}}", eyeImagePath);
        htmlTemplate = htmlTemplate.Replace("{{copyright-logo.svg}}", copyrightLogoPath);
        
        return htmlTemplate;
    }

    public string GenerateEmailSubject()
    {
        return string.Format(EmailSubjects.ForgetEmailSubject);
    }
}