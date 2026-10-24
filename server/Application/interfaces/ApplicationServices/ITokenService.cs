using Domain.Entities;

namespace Application.interfaces.ApplicationServices;

public interface ITokenService
{
    Task<string> GeneratePasswordResetTokenAsync(User user);
}