namespace Infrastructure.EmailService;

public class SmtpSettings
{
    public string? Server { get; set; }
    public int Port { get; set; }
    public string? Password { get; set; }
    public string? FromEmail { get; set; }
    public string? FromName { get; set; }
    public string? BaseUrl { get; set; }  
    public string? ResetPasswordPath { get; set; }
    public string? ForgetPasswordHtmlPath { get; set; }
}