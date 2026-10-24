using Application.ApplicationResult;
using Application.interfaces.ApplicationServices;
using Application.interfaces.ExternalServices;
using Application.interfaces.Repositories;
using Application.mappedDto;
using Application.mapperEntity;
using Application.Services;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Moq;

namespace test;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _mockUserRepo;
    private readonly Mock<ITokenService> _mockTokenService;
    private readonly Mock<IEmailService> _mockEmailService;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _mockUserRepo = new Mock<IUserRepository>();
        _mockTokenService = new Mock<ITokenService>();
        _mockEmailService = new Mock<IEmailService>();
        _userService = new UserService(_mockUserRepo.Object, _mockTokenService.Object, _mockEmailService.Object);
    }

    [Fact]
    public async Task LoginAsync_Success_ReturnsSuccessWithUser()
    {
        // Arrange
        var user = new User { UserName = "testuser", Email = "test@example.com" };
        var loginDto = new LoginDto { Identifier = "test@example.com", Password = "Password123@", IsEmail = true };

        _mockUserRepo.Setup(x => x.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
        _mockUserRepo.Setup(x => x.IsLockedOutAsync(It.IsAny<User>())).ReturnsAsync(false);
        _mockUserRepo.Setup(x => x.CheckPasswordSignInAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(true);

        var expectedResult =Result<User>.Success(user);

        // Act
        var actualResult = await _userService.LoginAsync(loginDto);

        // Assert
        Assert.Equal(expectedResult.Status, actualResult.Status);
        Assert.Equal(expectedResult.Value, actualResult.Value);
    }

    [Fact]
    public async Task LoginAsync_Fail_InvalidEmail_ReturnsUserNotFound()
    {
        // Arrange
        var loginDto = new LoginDto { Identifier = "testfail@example.com", Password = "Password123@", IsEmail = true };

        _mockUserRepo.Setup(x => x.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync((User?)default);
        _mockUserRepo.Setup(x => x.IsLockedOutAsync(It.IsAny<User>())).ReturnsAsync(false);
        _mockUserRepo.Setup(x => x.CheckPasswordSignInAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(true);

        var expectedResult = Result<User>.Failure(ResultStatus.UserNotFound);

        // Act
        var actualResult = await _userService.LoginAsync(loginDto);

        // Assert
        Assert.Equal(expectedResult.Status, actualResult.Status);
        Assert.Null(actualResult.Value);
    }

    [Fact]
    public async Task LoginAsync_Fail_InvalidUsername_ReturnsUserNotFound()
    {
        // Arrange
        var loginDto = new LoginDto { Identifier = "testuserfail", Password = "Password123@", IsEmail = false };

        _mockUserRepo.Setup(x => x.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync((User?)default);
        _mockUserRepo.Setup(x => x.IsLockedOutAsync(It.IsAny<User>())).ReturnsAsync(false);
        _mockUserRepo.Setup(x => x.CheckPasswordSignInAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(true);

        var expectedResult =  Result<User>.Failure(ResultStatus.UserNotFound);

        // Act
        var actualResult = await _userService.LoginAsync(loginDto);

        // Assert
        Assert.Equal(expectedResult.Status, actualResult.Status);
        Assert.Null(actualResult.Value);
    }

    [Fact]
    public async Task LoginAsync_Fail_InvalidPassword_ReturnsWrongPassword()
    {
        // Arrange
        var user = new User { UserName = "testuser", Email = "test@example.com" };
        var loginDto = new LoginDto { Identifier = "test@example.com", Password = "WrongPassword", IsEmail = true };

        _mockUserRepo.Setup(x => x.GetByEmailAsync(user.Email)).ReturnsAsync(user);
        _mockUserRepo.Setup(x => x.IsLockedOutAsync(user)).ReturnsAsync(false);
        _mockUserRepo.Setup(x => x.CheckPasswordSignInAsync(user, loginDto.Password)).ReturnsAsync(false);

        var expectedResult =Result<User>.Failure(ResultStatus.WrongPassword);

        // Act
        var actualResult = await _userService.LoginAsync(loginDto);

        // Assert
        Assert.Equal(expectedResult.Status, actualResult.Status);
        Assert.Null(actualResult.Value);
    }

    [Fact]
    public async Task LoginAsync_Fail_UserLockedOut_ReturnsUserLockedOut()
    {
        // Arrange
        var user = new User { UserName = "testuser", Email = "test@example.com" };
        var loginDto = new LoginDto { Identifier = "testuser", Password = "Password123@", IsEmail = false };

        _mockUserRepo.Setup(x => x.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync(user);
        _mockUserRepo.Setup(x => x.IsLockedOutAsync(It.IsAny<User>())).ReturnsAsync(true);
        _mockUserRepo.Setup(x => x.CheckPasswordSignInAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(true);

        var expectedResult = Result<User>.Failure(ResultStatus.UserLockedOut);

        // Act
        var actualResult = await _userService.LoginAsync(loginDto);

        // Assert
        Assert.Equal(expectedResult.Status, actualResult.Status);
        Assert.Null(actualResult.Value);
    }

    [Fact]
    public async Task LoginAsync_Fail_EmptyUsername_ThrowsBadUsageException()
    {
        // Arrange
        var loginDto = new LoginDto { Identifier = "", Password = "Password123@", IsEmail = false };

        // Act & Assert
        await Assert.ThrowsAsync<BadUsageException>(() => _userService.LoginAsync(loginDto));
    }

    [Fact]
    public async Task LoginAsync_Fail_EmptyEmail_ThrowsBadUsageException()
    {
        // Arrange
        var loginDto = new LoginDto { Identifier = "", Password = "Password123@", IsEmail = true };

        // Act & Assert
        await Assert.ThrowsAsync<BadUsageException>(() => _userService.LoginAsync(loginDto));
    }

    [Fact]
    public async Task SignUpAsync_Fail_UserWithSameUsernameExists_ReturnsUserAlreadyExists()
    {
        // Arrange
        var existingUser = new User { UserName = "Test", Email = "test@example.com" };
        var signupDto = new SignupDto
        {
            Username = "Test",
            Firstname = "Test",
            Lastname = "Test",
            Email = "testnotequal@example.com",
            DateOfBirth = DateOnly.MinValue,
            Password = "Password123@",
            Gender = Gender.Male
        };

        _mockUserRepo.Setup(x => x.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync(existingUser);

        var expectedResult = Result<User>.Failure(ResultStatus.UserAlreadyExists);

        _mockUserRepo.Setup(x => x.ValidatePasswordAsync(It.IsAny<User>(), signupDto.Password))
    .ReturnsAsync(true);

        // Act
        var actualResult = await _userService.SignUpAsync(signupDto);

        // Assert
        Assert.Equal(expectedResult.Status, actualResult.Status);
        Assert.Null(actualResult.Value);
    }

    [Fact]
    public async Task SignUpAsync_Fail_UserWithSameEmailExists_ReturnsUserAlreadyExists()
    {
        // Arrange
        var existingUser = new User { UserName = "Test", Email = "test@example.com" };
        var signupDto = new SignupDto
        {
            Username = "Testnotequal",
            Firstname = "Test",
            Lastname = "Test",
            Email = "test@example.com",
            DateOfBirth = DateOnly.MinValue,
            Password = "Password123@",
            Gender = Gender.Male
        };
        _mockUserRepo.Setup(x => x.ValidatePasswordAsync(It.IsAny<User>(), signupDto.Password))
    .ReturnsAsync(true);

        _mockUserRepo.Setup(x => x.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(existingUser);

        var expectedResult = Result<User>.Failure(ResultStatus.UserAlreadyExists);

        // Act
        var actualResult = await _userService.SignUpAsync(signupDto);

        // Assert
        Assert.Equal(expectedResult.Status, actualResult.Status);
        Assert.Null(actualResult.Value);
    }

    [Fact]
    public async Task SignUpAsync_Fail_DateOfBirthIncorrect_DateIsFuture_ReturnsInvalidDateOfBirth()
    {
        // Arrange
        var signupDto = new SignupDto
        {
            Username = "Testnotequal",
            Firstname = "Test",
            Lastname = "Test",
            Email = "test@example.com",
            DateOfBirth = DateOnly.MaxValue,
            Password = "1sA.5",
            Gender = Gender.Male
        };
        var expectedResult = Result<User>.Failure(ResultStatus.InvalidDateOfBirth);
        _mockUserRepo.Setup(x => x.ValidatePasswordAsync(It.IsAny<User>(), signupDto.Password))
    .ReturnsAsync(true);

        // Act
        var actualResult = await _userService.SignUpAsync(signupDto);

        // Assert
        Assert.Equal(expectedResult.Status, actualResult.Status);
        Assert.Null(actualResult.Value);
    }

    [Fact]
    public async Task SignUpAsync_Fail_Passwordinssufficent_RequiredLengthFail_ReturnsUserAlreadyExists()
    {
        // Arrange
        var signupDto = new SignupDto
        {
            Username = "Testnotequal",
            Firstname = "Test",
            Lastname = "Test",
            Email = "test@example.com",
            DateOfBirth = DateOnly.MinValue,
            Password = "1sA.5",
            Gender = Gender.Male
        };
        var expectedResult = Result<User>.Failure(ResultStatus.InvalidPassword);

        // Act
        var actualResult = await _userService.SignUpAsync(signupDto);

        // Assert
        Assert.Equal(expectedResult.Status, actualResult.Status);
        Assert.Null(actualResult.Value);
    }

    [Fact]
    public async Task SignUpAsync_Fail_Passwordinssufficent_NoCapitalLetter_ReturnsUserAlreadyExists()
    {
        // Arrange
        var signupDto = new SignupDto
        {
            Username = "Testnotequal",
            Firstname = "Test",
            Lastname = "Test",
            Email = "test@example.com",
            DateOfBirth = DateOnly.MinValue,
            Password = "sobhan123.",
            Gender = Gender.Male
        };
        var expectedResult = Result<User>.Failure(ResultStatus.InvalidPassword);

        // Act
        var actualResult = await _userService.SignUpAsync(signupDto);

        // Assert
        Assert.Equal(expectedResult.Status, actualResult.Status);
        Assert.Null(actualResult.Value);
    }

    [Fact]
    public async Task SignUpAsync_InvalidPassword_MissingCapitalLetter_ReturnsInvalidPassword()
    {
        // Arrange
        var signupDto = new SignupDto
        {
            Username = "Testnotequal",
            Firstname = "Test",
            Lastname = "Test",
            Email = "test@example.com",
            DateOfBirth = DateOnly.MinValue,
            Password = "SOBHAN.123",
            Gender = Gender.Male
        };
        var expectedResult = Result<User>.Failure(ResultStatus.InvalidPassword);

        // Act
        var actualResult = await _userService.SignUpAsync(signupDto);

        // Assert
        Assert.Equal(expectedResult.Status, actualResult.Status);
        Assert.Null(actualResult.Value);
    }

    [Fact]
    public async Task SignUpAsync_InvalidPassword_MissingLowercaseLetter_ReturnsInvalidPassword()
    {
        // Arrange
        var signupDto = new SignupDto
        {
            Username = "Testnotequal",
            Firstname = "Test",
            Lastname = "Test",
            Email = "test@example.com",
            DateOfBirth = DateOnly.MinValue,
            Password = "SOBHAN.ala",
            Gender = Gender.Male
        };
        var expectedResult = Result<User>.Failure(ResultStatus.InvalidPassword);

        // Act
        var actualResult = await _userService.SignUpAsync(signupDto);

        // Assert
        Assert.Equal(expectedResult.Status, actualResult.Status);
        Assert.Null(actualResult.Value);
    }

    [Fact]
    public async Task SignUpAsync_InvalidPassword_MissingDigit_ReturnsInvalidPassword()
    {
        // Arrange
        var signupDto = new SignupDto
        {
            Username = "Testnotequal",
            Firstname = "Test",
            Lastname = "Test",
            Email = "test@example.com",
            DateOfBirth = DateOnly.MinValue,
            Password = "SOBHANala123",
            Gender = Gender.Male
        };
        var expectedResult = Result<User>.Failure(ResultStatus.InvalidPassword);

        // Act
        var actualResult = await _userService.SignUpAsync(signupDto);

        // Assert
        Assert.Equal(expectedResult.Status, actualResult.Status);
        Assert.Null(actualResult.Value);
    }

    [Fact]
    public async Task SignUpAsync_InvalidPassword_MissingNonAlphanumericCharacter_ReturnsInvalidPassword()
    {
        // Arrange
        var signupDto = new SignupDto
        {
            Username = "Testnotequal",
            Firstname = "Test",
            Lastname = "Test",
            Email = "test@domain",  // Missing top-level domain (e.g., .com)
            DateOfBirth = DateOnly.MinValue,
            Password = "ValidPassword123!",
            Gender = Gender.Male
        };
        var expectedResult = Result<User>.Failure(ResultStatus.InvalidEmail);
        _mockUserRepo.Setup(x => x.ValidatePasswordAsync(It.IsAny<User>(), signupDto.Password))
        .ReturnsAsync(true);
        // Act
        var actualResult = await _userService.SignUpAsync(signupDto);

        // Assert
        Assert.Equal(expectedResult.Status, actualResult.Status);
        Assert.Null(actualResult.Value);
    }

    [Fact]
    public async Task SignUpAsync_Fail_InvalidEmail_MissingAtSymbol_ReturnsInvalidEmail()
    {
        // Arrange
        var signupDto = new SignupDto
        {
            Username = "Testnotequal",
            Firstname = "Test",
            Lastname = "Test",
            Email = "invalidemail.com",  // Missing @ symbol
            DateOfBirth = DateOnly.MinValue,
            Password = "SOBHANala123!",
            Gender = Gender.Male
        };
        var expectedResult = Result<User>.Failure(ResultStatus.InvalidEmail);

        _mockUserRepo.Setup(x => x.ValidatePasswordAsync(It.IsAny<User>(), signupDto.Password))
    .ReturnsAsync(true);

        // Act
        var actualResult = await _userService.SignUpAsync(signupDto);

        // Assert
        Assert.Equal(expectedResult.Status, actualResult.Status);
        Assert.Null(actualResult.Value);
    }

    [Fact]
    public async Task SignUpAsync_InvalidEmail_Noadsine_ReturnsInvalidEmail()
    {
        // Arrange
        var signupDto = new SignupDto
        {
            Username = "Testnotequal",
            Firstname = "Test",
            Lastname = "Test",
            Email = "test..emailomain.com",  // Invalid: consecutive dots
            DateOfBirth = DateOnly.MinValue,
            Password = "SOBHANala123!",
            Gender = Gender.Male
        };
        var expectedResult = Result<User>.Failure(ResultStatus.InvalidEmail);

        // Act
        _mockUserRepo.Setup(x => x.ValidatePasswordAsync(It.IsAny<User>(), signupDto.Password))
    .ReturnsAsync(true);
       
        _mockUserRepo.Setup(x => x.AddAsync(signupDto.ToUser(), signupDto.Password)).
            ReturnsAsync(It.IsAny<User>);
        
        var actualResult = await _userService.SignUpAsync(signupDto);

        // Assert
        Assert.Equal(expectedResult.Status, actualResult.Status);
        Assert.Null(actualResult.Value);
    }

    [Fact]
    public async Task SignUpAsync_Fail_InvalidName_LongName_ReturnsInvalidFirstName()
    {
        // Arrange
        var signupDto = new SignupDto
        {
            Username = "Test",
            Firstname = "0123456789012345678902345678941256789045678903456789456789023456789345678",//anything more than 20 is invalid
            Lastname = "Test",
            Email = "test.email@domain.com",
            DateOfBirth = DateOnly.MinValue,
            Password = "Ala.13495782",
            Gender = Gender.Male
        };
        var expectedResult = Result<User>.Failure(ResultStatus.InvalidFirstname);
        _mockUserRepo.Setup(x => x.ValidatePasswordAsync(It.IsAny<User>(), signupDto.Password))
        .ReturnsAsync(true);
        // Act
        var actualResult = await _userService.SignUpAsync(signupDto);

        // Assert
        Assert.Equal(expectedResult.Status, actualResult.Status);
        Assert.Null(actualResult.Value);
    }

    [Fact]
    public async Task SignUpAsync_Fail_InvalidName_LongLastName_ReturnsInvalidLastname()
    {
        // Arrange

        var signupDto = new SignupDto
        {
            Username = "Test",
            Firstname = "Test",
            Lastname = "01234567890123456789001234567890123456789001234567890",//anything more than 50 is invalid
            Email = "test.email@domain.com",
            DateOfBirth = DateOnly.MinValue,
            Password = "ValidPassword123!",
            Gender = Gender.Male
        };
        
        var expectedResult = Result<User>.Failure(ResultStatus.InvalidLastname);
        _mockUserRepo.Setup(x => x.ValidatePasswordAsync(It.IsAny<User>(), signupDto.Password))
        .ReturnsAsync(true); 
        // Act
        var actualResult = await _userService.SignUpAsync(signupDto);

        // Assert
        Assert.Equal(expectedResult.Status, actualResult.Status);
        Assert.Null(actualResult.Value);
    }

    [Fact]
    public async Task SignUpAsync_Fail_InvalidSignupInfo_UsernameBadusage_ReturnsFailure()
    {
        // Arrange
        var invalidSignupDto = new SignupDto
        {
            Username = "", // Invalid username
            Email = "test@domain.com",
            Password = "ValidPass123!",
            Firstname = "John",
            Lastname = "Doe",
            DateOfBirth = DateOnly.MinValue,
            Gender = Gender.Male
        };

        // Assert
        await Assert.ThrowsAsync<BadUsageException>(() => _userService.SignUpAsync(invalidSignupDto));

    }

    [Fact]
    public async Task SignUpAsync_Fail_InvalidSignupInfo_EmailBadusage_ReturnsFailure()
    {
        // Arrange
        var invalidSignupDto = new SignupDto
        {
            Username = "test",
            Email = "",
            Password = "ValidPass123!",
            Firstname = "John",
            Lastname = "Doe",
            DateOfBirth = DateOnly.MinValue,
            Gender = Gender.Male
        };

        // Assert
        await Assert.ThrowsAsync<BadUsageException>(() => _userService.SignUpAsync(invalidSignupDto));

    }
    [Fact]
    public async Task SignUpAsync_Fail_InvalidSignupInfo_FirstnameBadusage_ReturnsFailure()
    {
        // Arrange
        var invalidSignupDto = new SignupDto
        {
            Username = "Test",
            Email = "test@domain.com",
            Password = "ValidPass123!",
            Firstname = "",
            Lastname = "Doe",
            DateOfBirth = DateOnly.MinValue,
            Gender = Gender.Male
        };

        // Assert
        await Assert.ThrowsAsync<BadUsageException>(() => _userService.SignUpAsync(invalidSignupDto));

    }

    [Fact]
    public async Task SignUpAsync_Fail_InvalidSignupInfo_PasswordBadusage_ReturnsFailure()
    {
        // Arrange
        var invalidSignupDto = new SignupDto
        {
            Username = "Test",
            Email = "test@domain.com",
            Password = "",
            Firstname = "joe",
            Lastname = "Doe",
            DateOfBirth = DateOnly.MinValue,
            Gender = Gender.Male
        };

        // Assert
        await Assert.ThrowsAsync<BadUsageException>(() => _userService.SignUpAsync(invalidSignupDto));

    }


    [Fact]
    public async Task SignUpAsync_Fail_InvalidSignupInfo_GenderBadusage_ReturnsFailure()
    {
        // Arrange
        var invalidSignupDto = new SignupDto
        {
            Username = "Test",
            Email = "test@domain.com",
            Password = "ValidPass123!",
            Firstname = "sjoe",
            Lastname = null,

        };

        // Assert
        await Assert.ThrowsAsync<BadUsageException>(() => _userService.SignUpAsync(invalidSignupDto));

    }

    [Fact]
    public async Task SignUpAsync_Fail_AddUserReturnsNull_ThrowsUnexpectedException()
    {
        // Arrange
        var signupDto = new SignupDto
        {
            Username = "testuser",
            Email = "test@domain.com",
            Password = "ValidPass123!",
            Firstname = "John",
            Lastname = "Doe",
            DateOfBirth = DateOnly.MinValue,
            Gender = Gender.Male
        };

        // Mock GetByEmailAsync and GetByUsernameAsync to return null (user does not exist)
        _mockUserRepo.Setup(repo => repo.GetByEmailAsync(signupDto.Email))
            .ReturnsAsync(null as User);
        _mockUserRepo.Setup(repo => repo.GetByUsernameAsync(signupDto.Username))
            .ReturnsAsync(null as User);
        _mockUserRepo.Setup(x => x.ValidatePasswordAsync(It.IsAny<User>(), signupDto.Password)).ReturnsAsync(true);


        // Mock AddAsync to return null (simulating failure)
        _mockUserRepo.Setup(repo => repo.AddAsync(signupDto.ToUser(), signupDto.Password))
            .ReturnsAsync(null as User);


        // Act & Assert
        await Assert.ThrowsAsync<UnexpectedException>(() => _userService.SignUpAsync(signupDto));
    }

    [Fact]
    public async Task SignUpAsync_Success_ReturnsSuccessWithUser()
    {
        // Arrange
        var signupDto = new SignupDto
        {
            Username = "testuser",
            Email = "test@domain.com",
            Password = "ValidPass123!",
            Firstname = "John",
            Lastname = "Doe",
            DateOfBirth = DateOnly.MinValue,
            Gender = Gender.Male
        };

        var newUser = new User { UserName = signupDto.Username, Email = signupDto.Email };

        _mockUserRepo.Setup(x => x.ValidatePasswordAsync(It.IsAny<User>(), signupDto.Password))
        .ReturnsAsync(true);

        _mockUserRepo.Setup(repo => repo.GetByEmailAsync(signupDto.Email))
            .ReturnsAsync((User?)null);
        _mockUserRepo.Setup(repo => repo.GetByUsernameAsync(signupDto.Username))
            .ReturnsAsync((User?)null);

        _mockUserRepo.Setup(repo => repo.AddAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(newUser);

        // Act
        var actualResult = await _userService.SignUpAsync(signupDto);

        // Assert
        Assert.Equal(ResultStatus.Succeeded, actualResult.Status);
        Assert.NotNull(actualResult.Value);
        Assert.Equal(newUser.UserName, actualResult.Value.UserName);
        Assert.Equal(newUser.Email, actualResult.Value.Email);
        Assert.Equal(newUser.FirstName, actualResult.Value.FirstName);
        Assert.Equal(newUser.LastName, actualResult.Value.LastName);
        Assert.Equal(newUser.DateOfBirth, actualResult.Value.DateOfBirth);
        Assert.Equal(newUser.Gender, actualResult.Value.Gender);


    }

    [Fact]
    public async Task GeneratePasswordResetTokenAsync_ShouldReturnUserNotFound_WhenUserDoesNotExist()
    {
        string email = "test@example.com";
        _mockUserRepo.Setup(repo => repo.GetByEmailAsync(email))
            .ReturnsAsync(null as User);

        // Act
        var result = await _userService.GeneratePasswordResetTokenAsync(email);

        // Assert
        Assert.Equal(ResultStatus.UserNotFound, result.Status);
    }

    [Fact]
    public async Task GeneratePasswordResetTokenAsync_ShouldThrowBadUsageException_WhenEmailIsNullOrWhitespace()
    {
        string email = "";

        await Assert.ThrowsAsync<BadUsageException>(() => _userService.GeneratePasswordResetTokenAsync(email));
    }

    [Fact]
    public async Task GeneratePasswordResetTokenAsync_ShouldSendEmail_WhenUserExists()
    {
        var email = "test@example.com";
        var user = new User { Email = email, UserName = "TestUser" , FirstName = "Test", Gender = Gender.Male};
        var token = "generatedToken";
        var subject = "Reset Password Subject";
        var body = "Reset Password Body";

        _mockUserRepo.Setup(repo => repo.GetByEmailAsync(email)).ReturnsAsync(user);
        _mockTokenService.Setup(service => service.GeneratePasswordResetTokenAsync(user)).ReturnsAsync(token);
        _mockEmailService.Setup(service => service.GenerateEmailSubject()).Returns(subject);
        _mockEmailService.Setup(service => service.GenerateEmailBody(token, email, user.FirstName, user.Gender)).Returns(body);
        
        var result = await _userService.GeneratePasswordResetTokenAsync(email);
        _mockEmailService.Verify(service => service.SendEmailAsync(user.Email!, subject, body), Times.Once);
        Assert.Equal(ResultStatus.Succeeded, result.Status);
    }

    [Fact]
    public async Task ResetPasswordAsync_ShouldThrowBadUsageException_WhenResetPasswordEmailisNull()
    {
        var ForgetPasswordDto = new ResetPasswordDto
        {
            Email = "",
            Token = "test",
            NewPassword = "test"
        };

        await Assert.ThrowsAsync<BadUsageException>(() => _userService.ResetPasswordAsync(ForgetPasswordDto));
    }

    [Fact]
    public async Task ResetPasswordAsync_ShouldThrowBadUsageException_WhenResetPasswordTokenisNull()
    {
        var ForgetPasswordDto = new ResetPasswordDto
        {
            Email = "test",
            Token = "",
            NewPassword = "test"
        };

        await Assert.ThrowsAsync<BadUsageException>(() => _userService.ResetPasswordAsync(ForgetPasswordDto));
    }

    [Fact]
    public async Task ResetPasswordAsync_ShouldThrowBadUsageException_WhenResetPasswordNewPasswordisNull()
    {
        var resetpassdto = new ResetPasswordDto
        {
            Email = "test",
            Token = "test",
            NewPassword = ""
        };

        await Assert.ThrowsAsync<BadUsageException>(() => _userService.ResetPasswordAsync(resetpassdto));
    }

    [Fact]
    public async Task ResetPasswordAsync_ShouldReturnUserNotFound_WhenEmailIsNotInDataBase()
    {
        var user = new User
        {
            UserName = "test",
            Email = "test@domain.com",
            PasswordHash = "test",
            DateOfBirth = DateOnly.MinValue,
            Gender = Gender.Male,
            FirstName = "test",
            LastName = "test"
        };
   

        var resetPasswordDto = new ResetPasswordDto
        {
            Email = "test@gmail.com",
            Token = "test",
            NewPassword = "Test@123"
        };
        _mockUserRepo.Setup(x => x.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(null as User);
        _mockUserRepo.Setup(x => x.ValidatePasswordAsync(user, resetPasswordDto.NewPassword)).ReturnsAsync(true);
        _mockUserRepo.Setup(x => x.ResetPasswordAsync(user, resetPasswordDto.Token,resetPasswordDto.NewPassword)).ReturnsAsync(true);

        var expectedResult = Result.Failure(ResultStatus.UserNotFound);
        var actualResult =await _userService.ResetPasswordAsync(resetPasswordDto);

        Assert.Equal(expectedResult.Status, actualResult.Status);
    }

    [Fact]
    public async Task ResetPasswordAsync_ShouldReturnPasswordInvalid_WhenPasswordIsInvalid()
    {
        var user = new User
        {
            UserName = "test",
            Email = "test@gmail.com",
            PasswordHash = "test",
            DateOfBirth = DateOnly.MinValue,
            Gender = Gender.Male,
            FirstName = "test",
            LastName = "test"
        };


        var resetPasswordDto = new ResetPasswordDto
        {
            Email = "test@gmail.com",
            Token = "test",
            NewPassword = "test123"
        };
        _mockUserRepo.Setup(x => x.GetByEmailAsync(user.Email)).ReturnsAsync(user);
        _mockUserRepo.Setup(x => x.ValidatePasswordAsync(user, resetPasswordDto.NewPassword)).ReturnsAsync(false);
        _mockUserRepo.Setup(x => x.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.NewPassword)).ReturnsAsync(true);

        var expectedResult = Result.Failure(ResultStatus.InvalidPassword);
        var actualResult = await _userService.ResetPasswordAsync(resetPasswordDto);

        Assert.Equal(expectedResult.Status, actualResult.Status);
    }

    [Fact]
    public async Task ResetPasswordAsync_ShouldReturnInvalidToken_WhenResetPasswordAsync()
    {
        var user = new User
        {
            UserName = "test",
            Email = "test@domain.com",
            PasswordHash = "test",
            DateOfBirth = DateOnly.MinValue,
            Gender = Gender.Male,
            FirstName = "test",
            LastName = "test"
        };
        var resetPasswordDto = new ResetPasswordDto
        {
            Email = "test@domain.com",
            Token = "test",
            NewPassword = "test123"
        };
        _mockUserRepo.Setup(x => x.GetByEmailAsync(user.Email)).ReturnsAsync(user);
        _mockUserRepo.Setup(x => x.ValidatePasswordAsync(user, resetPasswordDto.NewPassword)).ReturnsAsync(true);
        _mockUserRepo.Setup(x => x.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.NewPassword)).ReturnsAsync(false);

        var expectedResult = Result.Failure(ResultStatus.InvalidToken);
        var actualResult = await _userService.ResetPasswordAsync(resetPasswordDto);

        Assert.Equal(expectedResult.Status, actualResult.Status);
    }

}
