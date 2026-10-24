using Application.ApplicationResult;
using Domain.Entities;
using Domain.Utils;

namespace Application.interfaces.ApplicationServices;

public interface IFollowService
{
    Task<Result> FollowAsync(string? followerUsername, string? followeeUsername);
    Task<Result> UnfollowAsync(string? followerUsername, string? followeeUsername);
    Task<Result<PaginatedList<User>>> GetFollowersAsync(string? username, int pageNumber, int pageSize);
    Task<Result<PaginatedList<User>>> GetFollowingsAsync(string? username, int pageNumber, int pageSize);
}