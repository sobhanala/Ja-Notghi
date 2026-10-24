using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Requests;

public class LoginRequest
{
    [Required]
    public string? Identifier { get; set; }
    [Required]
    public string? Password { get; set; }
    public bool IsEmail { get; set; }
}