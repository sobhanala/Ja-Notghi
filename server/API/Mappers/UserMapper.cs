using API.DTOs.Requests;
using API.DTOs.ResponseDtos;
using Application.mappedDto;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.Data;
using LoginRequest = API.DTOs.Requests.LoginRequest;

namespace API.Mappers;

public static class UserMapper
{
    public static LoginDto ToUserLoginDto(this LoginRequest loginRequest)
    {
        return new LoginDto
        {
            Identifier = loginRequest.Identifier,
            Password = loginRequest.Password,
            IsEmail = loginRequest.IsEmail
        };
    }

    public static SignupDto ToUserSignupDto(this SignupRequest signupRequest)
    {
        return new SignupDto
        {
            Username = signupRequest.Username,
            Email = signupRequest.Email,
            Firstname = signupRequest.Firstname,
            Lastname = signupRequest.Lastname,
            Password = signupRequest.Password,
            Gender = signupRequest.Gender,
            DateOfBirth = signupRequest.DateOfBirth,
        };
    }

    public static SignupResponse ToUserResponse(this User user)
    {
        return new SignupResponse
        {
            Firstname = user.FirstName,
            Lastname = user.LastName,
            Username = user.UserName,
            Email = user.Email,
            DateOfBirth = user.DateOfBirth,
            Gender = user.Gender
        };
    }

    public static ResetPasswordDto ToResetPasswordDto(this ResetPasswordRequest resetPasswordRequest)
    {
        return new ResetPasswordDto
        {
            Email = resetPasswordRequest.Email,
            NewPassword = resetPasswordRequest.NewPassword,
            Token = resetPasswordRequest.ResetCode,
        };
    }
    
    public static UserFollowDto ToUserFollowDto(this User user)
    {
        return new UserFollowDto
        {
            Firstname = user.FirstName,
            Lastname = user.LastName,
            Username = user.UserName
        };
    }
}
