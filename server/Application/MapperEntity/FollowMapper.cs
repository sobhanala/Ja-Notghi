using Domain.Entities;

namespace Application.mapperEntity;

public static class FollowMapper
{
    public static Follow ToFollow(string? followerUsername, string? followeeUsername, DateTime followedAt)
    {
        return new Follow
        {
            FollowerUserName = followerUsername,
            FollowedUserName = followeeUsername,
            FollowedAt = followedAt
        };
    }
}