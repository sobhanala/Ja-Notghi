using Domain.Enums;

namespace API.DTOs.ResponseDtos;

public class SignupResponse
{
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Username {  get; set; }
    public string? Email { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public Gender? Gender { get; set; }
}