namespace Domain.Entities;

public class Follow
{
    public string? FollowerUserName { get; set; }
    public string? FollowedUserName { get; set; }
    public DateTime? FollowedAt { get; set; }
}