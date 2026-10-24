using API.TokenService;

namespace API.Extensions;

public static class TokenConfigurationExtensions
{
    public static void ConfigureJwtSettings(this IServiceCollection services, IConfiguration configuration)
    {
        var issuer = configuration.GetValue<string>("JwtSettings:Issuer")!;
        var audience = configuration.GetValue<string>("JwtSettings:Audience")!;
        var tokenExpirationInMinutes = configuration.GetValue<int>("JwtSettings:TokenExpirationInMinutes");
        var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY")!;
        
        services.Configure<JwtSettings>(options =>
        {
            options.Issuer = issuer;
            options.Audience = audience;
            options.SecretKey = jwtKey;
            options.TokenExpirationInMinutes = tokenExpirationInMinutes;
        });
    }
}