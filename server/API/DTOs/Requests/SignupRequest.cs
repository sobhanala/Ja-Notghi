using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace API.DTOs.Requests;

public class SignupRequest
{
    [Required]
    public string? Firstname { get; set; }
    [Required]
    public string? Lastname { get; set; }
    [Required]
    public string? Username {  get; set; }
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
    [Required]
    public DateOnly? DateOfBirth { get; set; }
    public Gender Gender { get; set; } = Gender.NotImportant;
}