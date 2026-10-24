using Application.ApplicationResult;
using Application.mappedDto;
using Domain.Entities;

namespace Application.interfaces.ApplicationServices;

public interface IUserService
{
    Task<Result<User>> LoginAsync(LoginDto loginDto);
    Task<Result<User>> SignUpAsync(SignupDto signupInfo);
    Task<Result> GeneratePasswordResetTokenAsync(string email);
    Task<Result> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
}