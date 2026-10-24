using Application.interfaces.Repositories;
using Domain.Entities;
using Domain.Utils;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class FollowRepository : IFollowRepository
{
    private readonly ApplicationDbContext _context;

    public FollowRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAsync(Follow follow)
    {
        await _context.Follows.AddAsync(follow);
        await _context.SaveChangesAsync();
    }
    
    public async Task RemoveAsync(Follow follow)
    {
        _context.Follows.Remove(follow);
        await _context.SaveChangesAsync();
    }
        
    public async Task<List<string>> GetFollowersAsync(string username, int pageNumber, int pageSize)
    {
        var followers = await _context.Follows
            .Where(f => f.FollowedUserName == username)
            .OrderByDescending(f => f.FollowedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(f => f.FollowerUserName!)
            .ToListAsync();
        return followers;
    }
    
    public async Task<List<string>> GetFollowingsAsync(string username, int pageNumber, int pageSize)
    {
        var followings = await _context.Follows
            .Where(f => f.FollowerUserName == username)
            .OrderByDescending(f => f.FollowedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(f => f.FollowedUserName!)
            .ToListAsync();
        return followings;
    }

    public async Task<Follow?> GetFollowAsync(string? followUsername, string? followedUsername)
    {
        return await _context.Follows.FirstOrDefaultAsync(f => f.FollowerUserName == followUsername &&
                           f.FollowedUserName == followedUsername);
        
    }
}