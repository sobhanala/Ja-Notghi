using Domain.Entities;

namespace Application.interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> AddAsync(User user,string password);
    Task<bool> CheckPasswordSignInAsync(User user, string password);
    Task <bool>ResetPasswordAsync(User user, string token,string newPassword);
    Task<bool> IsLockedOutAsync(User user);
    Task<bool> ValidatePasswordAsync(User user, string password);
    Task UpdateFollowersAndFollowingsAsync(User followee, User follower);
}