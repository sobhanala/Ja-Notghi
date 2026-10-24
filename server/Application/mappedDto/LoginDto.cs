namespace Application.mappedDto;

public class LoginDto
{
    public string? Identifier { get; set; }
    public string? Password { get; set; }
    public bool IsEmail { get; set; }
}