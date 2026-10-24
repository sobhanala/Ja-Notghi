using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Requests;

public class FollowRequest
{
    [Required]
    public string? FolloweeUsername { get; set; }
}