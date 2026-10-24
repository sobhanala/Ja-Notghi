using System.Text.RegularExpressions;
using Application.ApplicationResult;
using Application.interfaces;
using Application.interfaces.ApplicationServices;
using Application.interfaces.ExternalServices;
using Domain.Entities;
using Application.mappedDto;
using Application.mapperEntity;
using Application.interfaces.Repositories;
using Domain.Constant;
using Domain.Exceptions;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IEmailService _emailService;
    
    public UserService(IUserRepository userRepository,
        ITokenService tokenService,
        IEmailService emailService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _emailService = emailService;
    }
        
    public async Task<Result<User>> LoginAsync(LoginDto loginDto)
    {
        if (String.IsNullOrWhiteSpace(loginDto.Identifier) ||
            String.IsNullOrWhiteSpace(loginDto.Password))
        {
            throw new BadUsageException(nameof(LoginAsync));
        }
        
        var user = loginDto.IsEmail
            ? await _userRepository.GetByEmailAsync(loginDto.Identifier)
            : await _userRepository.GetByUsernameAsync(loginDto.Identifier);
        if (user == null)
        {
            return Result<User>.Failure(ResultStatus.UserNotFound);
        }
        
        if(await _userRepository.IsLockedOutAsync(user))
        {
            return Result<User>.Failure(ResultStatus.UserLockedOut);
        }

        var isPasswordCorrect = await _userRepository.CheckPasswordSignInAsync(user, loginDto.Password);
        if(await _userRepository.IsLockedOutAsync(user))
        {
            return Result<User>.Failure(ResultStatus.UserLockedOut);
        }
        if (isPasswordCorrect)
        {
            return Result<User>.Success(user);
        }
        return Result<User>.Failure(ResultStatus.WrongPassword);
    }

    private bool IsValidEmail(string email)
    {
        var emailRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(email, emailRegex) && email.Length <= IdentityRules.EmailMaxLength;
    }
    
    private bool IsValidUsername(string username)
    {
        var usernameRegex = @"^[a-zA-Z0-9._-]{3,}$";
        return Regex.IsMatch(username, usernameRegex) && username.Length <= IdentityRules.UsernameMaxLength;
    }

    private async Task<ResultStatus> ValidateSignupInfoAsync(SignupDto signupInfo)
    {
        if(String.IsNullOrWhiteSpace(signupInfo.Firstname) ||
           String.IsNullOrWhiteSpace(signupInfo.Lastname) ||
           String.IsNullOrWhiteSpace(signupInfo.Username) ||
           String.IsNullOrWhiteSpace(signupInfo.Email) ||
           String.IsNullOrWhiteSpace(signupInfo.Password)
          )
        {
            throw new BadUsageException(nameof(SignUpAsync));
        }
        var dummyUser = new User();
        if (!await _userRepository.ValidatePasswordAsync(dummyUser, signupInfo.Password))
        {
            return ResultStatus.InvalidPassword;
        }
        if (!(signupInfo.Firstname.Length <= IdentityRules.FirstNameMaxLength))
        {
            return ResultStatus.InvalidFirstname;
        }
        if (!(signupInfo.Lastname.Length <= IdentityRules.LastNameMaxLength))
        {
            return ResultStatus.InvalidLastname;
        }
        if (!IsValidEmail(signupInfo.Email))
        {
            return ResultStatus.InvalidEmail;
        }
        if (!(signupInfo.DateOfBirth <= DateOnly.FromDateTime(DateTime.Now)))
        {
            return ResultStatus.InvalidDateOfBirth;
        }
        if (!IsValidUsername(signupInfo.Username))
        {
            return ResultStatus.InvalidUsername;
        }

        return ResultStatus.Succeeded;
    }

    public async Task<Result<User>> SignUpAsync(SignupDto signupInfo)
    {
        var result = await ValidateSignupInfoAsync(signupInfo);
        if (result != ResultStatus.Succeeded)
        {
            return Result<User>.Failure(result);
        }
        
        if (await _userRepository.GetByEmailAsync(signupInfo.Email!) != null ||
            await _userRepository.GetByUsernameAsync(signupInfo.Username!) != null)
        {
            return Result<User>.Failure(ResultStatus.UserAlreadyExists);
        }

        var user = await _userRepository.AddAsync(signupInfo.ToUser(), signupInfo.Password!);
        if (user == null)
        {
            throw new UnexpectedException(nameof(SignUpAsync));
        }
        return Result<User>.Success(user);
    }

    public async Task<Result> GeneratePasswordResetTokenAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new BadUsageException(nameof(GeneratePasswordResetTokenAsync));
        }
        
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null)
        {
            return Result.Failure(ResultStatus.UserNotFound);
        }

        var token = await _tokenService.GeneratePasswordResetTokenAsync(user);
        
        var subject= _emailService.GenerateEmailSubject();
        var body = _emailService.GenerateEmailBody(token, user.Email!, user.FirstName!, user.Gender);
        
        await _emailService.SendEmailAsync(user.Email!,subject,body);

        return Result.Success(); 
    }
    
    public async Task<Result> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
    {
        if (String.IsNullOrWhiteSpace(resetPasswordDto.Email) ||
            String.IsNullOrWhiteSpace(resetPasswordDto.Token) ||
            String.IsNullOrWhiteSpace(resetPasswordDto.NewPassword)
            )
        {
            throw new BadUsageException(nameof(ResetPasswordAsync));
        }
        
        var user = await _userRepository.GetByEmailAsync(resetPasswordDto.Email);
        if (user == null)
        {
            return Result.Failure(ResultStatus.UserNotFound);  
        }
        
        if(!await _userRepository.ValidatePasswordAsync(user, resetPasswordDto.NewPassword))
        {
            return Result.Failure(ResultStatus.InvalidPassword);
        }

        var succeeded = await _userRepository.ResetPasswordAsync(user,
            resetPasswordDto.Token,
            resetPasswordDto.NewPassword);
        
        if (!succeeded)
        {
            return Result.Failure(ResultStatus.InvalidToken);
        }
        
        return Result.Success();
    }}