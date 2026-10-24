using Domain.Entities;
using Domain.Utils;

namespace Application.interfaces.Repositories;

public interface IFollowRepository
{
    Task AddAsync(Follow follow);
    Task RemoveAsync(Follow follow);
    Task<List<string>> GetFollowersAsync(string username, int pageNumber, int pageSize);
    Task<List<string>> GetFollowingsAsync(string username, int pageNumber, int pageSize);
    Task<Follow?> GetFollowAsync(string? followUsername, string? followedUsername);
}