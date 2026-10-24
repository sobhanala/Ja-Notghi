using Application.ApplicationResult;
using Application.interfaces.ApplicationServices;
using Application.interfaces.Repositories;
using Application.mapperEntity;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Utils;

namespace Application.Services;

public class FollowService : IFollowService
{
    private readonly IFollowRepository _followRepository;
    private readonly IUserRepository _userRepository;

    public FollowService(IFollowRepository followRepository, IUserRepository userRepository)
    {
        _followRepository = followRepository;
        _userRepository = userRepository;
    }

    public async Task<Result> FollowAsync(string? followerUsername, string? followeeUsername)
    {
        if (String.IsNullOrWhiteSpace(followerUsername) ||
            String.IsNullOrWhiteSpace(followeeUsername)
           )
        {
            throw new BadUsageException(nameof(FollowAsync));
        }
        
        var follower = await _userRepository.GetByUsernameAsync(followerUsername);
        var followee = await _userRepository.GetByUsernameAsync(followeeUsername);
        if (follower == null)
        {
            throw new BadUsageException(nameof(FollowAsync));
        }
        if(followee == null)
        {
            return Result.Failure(ResultStatus.UserNotFound);
        }
        
        if(followerUsername == followeeUsername)
        {
            return Result.Failure(ResultStatus.FollowYourself);
        }
        

        var follow = await _followRepository.GetFollowAsync(followerUsername, followeeUsername);
        if (follow!=null)
        {
            return Result.Failure(ResultStatus.FollowAlreadyExist);
        }
        
        await _followRepository.AddAsync(FollowMapper.ToFollow(followerUsername, followeeUsername, DateTime.UtcNow));

        follower.FollowingsCount ++;
        followee.FollowersCount ++;
        await _userRepository.UpdateFollowersAndFollowingsAsync(follower, followee);  

        return Result.Success();
    }

    public async Task<Result> UnfollowAsync(string? followerUsername, string? followeeUsername)
    {
        if (String.IsNullOrWhiteSpace(followerUsername) ||
            String.IsNullOrWhiteSpace(followeeUsername)
           )
        {
            throw new BadUsageException(nameof(FollowAsync));
        }
        
        var follower = await _userRepository.GetByUsernameAsync(followerUsername);
        var followee = await _userRepository.GetByUsernameAsync(followeeUsername);

        if (follower == null)
        {
            return Result.Failure(ResultStatus.UserNotFound);
        }
        if (followee == null)
        {
            return Result.Failure(ResultStatus.UserNotFound);
        }
        if (followee.Equals(follower))
        {
            return Result.Failure(ResultStatus.UnfollowYourself);
        }

        var follow = await _followRepository.GetFollowAsync(followerUsername, followeeUsername);
        if (follow == null)
        {
            return Result.Failure(ResultStatus.FollowNotFound);
        }
        
        await _followRepository.RemoveAsync(follow);

        follower.FollowingsCount--;
        followee.FollowersCount--;
        await _userRepository.UpdateFollowersAndFollowingsAsync(follower, followee);
        return Result.Success();
    }

    public async Task<Result<PaginatedList<User>>> GetFollowersAsync(string? username, int pageNumber, int pageSize)
    {
        if (String.IsNullOrWhiteSpace(username))
        {
            throw new BadUsageException(nameof(GetFollowersAsync));
        }
        var user = await _userRepository.GetByUsernameAsync(username);
        if (user == null)
        {
            return Result<PaginatedList<User>>.Failure(ResultStatus.UserNotFound);
        }
        var followers = await _followRepository.GetFollowersAsync(username, pageNumber, pageSize);
        var users = new List<User>();
        foreach (var follower in followers)
        {
            var u = await _userRepository.GetByUsernameAsync(follower);
            users.Add(u!);
        
        }
        var usersPaginatedList = new PaginatedList<User>
        {
            Items = users,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = user.FollowersCount,    
        };
        return Result<PaginatedList<User>>.Success(usersPaginatedList);
    }

    public async Task<Result<PaginatedList<User>>> GetFollowingsAsync(string? username, int pageNumber, int pageSize)
    {
        if (String.IsNullOrWhiteSpace(username))
        {
            throw new BadUsageException(nameof(GetFollowingsAsync));
        }
        var user = await _userRepository.GetByUsernameAsync(username);
        if (user == null)
        {
            return Result<PaginatedList<User>>.Failure(ResultStatus.UserNotFound);
        }
        var followings = await _followRepository.GetFollowingsAsync(username, pageNumber, pageSize);
        var users = new List<User>();
        foreach (var following in followings)
        {
            var u = await _userRepository.GetByUsernameAsync(following);
            users.Add(u!);
        }
        var usersPaginatedList = new PaginatedList<User>
        {
            Items = users,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = user.FollowingsCount
        };
        return Result<PaginatedList<User>>.Success(usersPaginatedList);
    }
}