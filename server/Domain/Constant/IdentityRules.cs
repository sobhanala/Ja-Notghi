namespace Domain.Constant;

public static class IdentityRules
{
    public const int FirstNameMaxLength = 50;
    public const int LastNameMaxLength = 50;
    public const int EmailMaxLength = 50;
    public const int UsernameMaxLength = 50;
    public const int PasswordMinLength = 6;
    public const int LockOutTime = 1;
    public const int MaxFailedAccessAttempts = 5;
    public const int TokenExpirationTime = 1;
}