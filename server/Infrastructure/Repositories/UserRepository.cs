using Application.interfaces;
using Application.interfaces.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Domain.Exceptions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Application.ApplicationResult;
using Microsoft.AspNetCore.Builder;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDbContext dbContext)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _dbContext = dbContext;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {        
        return await _userManager.Users.FirstOrDefaultAsync(user => user.Email == email);
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _userManager.Users.FirstOrDefaultAsync(user => user.UserName == username);
    }
    
    public async Task<bool> IsLockedOutAsync(User user)
    {
        return await _userManager.IsLockedOutAsync(user);
    }
    
    public async Task<bool> CheckPasswordSignInAsync(User user, string password)
    {
        var result = await _signInManager.CheckPasswordSignInAsync(user, password, true);
        if (result.IsLockedOut || result.IsNotAllowed || result.RequiresTwoFactor)
        {
            throw new UnexpectedException(nameof(CheckPasswordSignInAsync));
        }
        return result.Succeeded;
    }

    public async Task<bool> ValidatePasswordAsync(User user, string password)
    {
        var result = await _userManager.PasswordValidators
            .Single()
            .ValidateAsync(_userManager, user, password);
        return result.Succeeded;
    }

    public async Task<User?> AddAsync(User user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.Description));
            throw new UnexpectedException(nameof(AddAsync) + errors);
        }
        return user;
    }

    public async Task<bool> ResetPasswordAsync(User user, string token, string newPassword)
    {
        var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
        if (result.Succeeded)
        {
            return true;
        }
        if (result.Errors.Any(e => e.Code == "InvalidToken"))
        {
            return false;
        }
        
        var errors = string.Join("; ", result.Errors.Select(e => e.Description));
        throw new UnexpectedException(nameof(ResetPasswordAsync) + errors);
    }

    public async Task UpdateFollowersAndFollowingsAsync(User followee, User follower)
    {
       
        using (var transaction = await _dbContext.Database.BeginTransactionAsync())
        {
            
            try
            {
                _dbContext.Users.Update(follower);
                _dbContext.Users.Update(followee);
               
              
                await _dbContext.SaveChangesAsync();
               
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw  new UnexpectedException(nameof(UpdateFollowersAndFollowingsAsync));
                ;
            }
        }
    }

}