using Application.interfaces.ApplicationServices;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class TokenService : ITokenService
{
    private readonly UserManager<User> _userManager;

    public TokenService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<string> GeneratePasswordResetTokenAsync(User user) 
    {
        return await _userManager.GeneratePasswordResetTokenAsync(user);
    }
}