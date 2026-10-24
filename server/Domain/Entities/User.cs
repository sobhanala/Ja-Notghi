using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User: IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set;}
    public DateOnly? DateOfBirth { get; set; }
    public Gender? Gender { get; set; } 
    public int FollowersCount { get; set; }
    public int FollowingsCount { get; set; }
}