using Application.mappedDto;
using Domain.Entities;

namespace Application.mapperEntity;

public static class UserMapper
{
    public static User ToUser(this SignupDto signupDto)
    {
        return new User
        {
            UserName = signupDto.Username,
            Email = signupDto.Email,
            FirstName = signupDto.Firstname,
            LastName = signupDto.Lastname,
            Gender = signupDto.Gender,
            DateOfBirth = signupDto.DateOfBirth
        };
    }
}