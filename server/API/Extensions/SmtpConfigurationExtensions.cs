using Infrastructure.EmailService;

namespace API.Extensions;

public static class SmtpConfigurationExtensions
{
    public static void ConfigureSmtpSettings(this IServiceCollection services, IConfiguration configuration)
    {
        var server = configuration.GetValue<string>("SmtpSettings:Server");
        var port = configuration.GetValue<int>("SmtpSettings:Port");
        var fromName = configuration.GetValue<string>("SmtpSettings:FromName");
        var baseUrl = configuration.GetValue<string>("AppSettings:BaseUrl");
        var forgetPasswordHtmlPath = configuration.GetValue<string>("SmtpSettings:ForgetPasswordHtmlPath");
        var resetPasswordPath = configuration.GetValue<string>("AppSettings:ResetPasswordPath");
        var password = Environment.GetEnvironmentVariable("SMTP_PASSWORD")!;
        var fromEmail = Environment.GetEnvironmentVariable("SMTP_FROMEMAIL")!;

        services.Configure<SmtpSettings>(options =>
        {
            options.Server = server;
            options.Port = port;
            options.Password = password;
            options.FromEmail = fromEmail;
            options.FromName = fromName;
            options.BaseUrl =baseUrl;
            options.ResetPasswordPath = resetPasswordPath;
            options.ForgetPasswordHtmlPath = forgetPasswordHtmlPath;
        });
    }
}