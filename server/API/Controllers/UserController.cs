using Microsoft.AspNetCore.Mvc;
using API.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using API.DTOs.Requests;
using API.DTOs.ResponseTemplate;
using API.TokenService;
using Application.interfaces.ApplicationServices;
using LoginRequest = API.DTOs.Requests.LoginRequest;
using API.Controllers.Service;
using Application.ApplicationResult;

namespace API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly TokenGenerator _tokenGenerator;
    private readonly ILogger<UserController> _logger;
    private readonly UserControllerService _userControllerService;

    public UserController(IUserService userService,
        TokenGenerator tokenGenerator,
        ILogger<UserController> logger,
        UserControllerService userControllerService)
    {
        _userService = userService;
        _tokenGenerator = tokenGenerator;
        _logger = logger;
        _userControllerService = userControllerService;
    }

    private IActionResult HandleException(Exception e)
    {
        _logger.LogCritical(e.ToString());
        return StatusCode(StatusCodes.Status500InternalServerError,(new Response
        {
            StatusCode = ResponseStatus.UnExpectedError,
            Message = ResponseStatus.UnExpectedError.GetMessage(),
            Data = null
        }));
    }

    [HttpGet]
    [Authorize]
    public IActionResult GetAll()
    {
        return Ok("Test Authorization");
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
            try
            {
                var loginResult = await _userService.LoginAsync(loginRequest.ToUserLoginDto());
                if (loginResult.Status != ResultStatus.Succeeded)
                    return _userControllerService.LoginResponseGenerator(loginResult.Status, null);
                var token = _tokenGenerator.GenerateToken(loginResult.Value!.UserName!);
                return _userControllerService.LoginResponseGenerator(loginResult.Status, token);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
    }
    
    [HttpPost]
    public async Task<IActionResult> SignUp([FromBody] SignupRequest userSignupRequest)
    {
        try
        {
            var signupResult = await _userService.SignUpAsync(userSignupRequest.ToUserSignupDto());
            return _userControllerService.SignupResponseGenerator(signupResult.Status, signupResult.Value!);            
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> ForgetPassword([FromBody] ForgotPasswordRequest forgetPasswordRequest)
    {
        try
        {
            var tokenResult = await _userService.GeneratePasswordResetTokenAsync(forgetPasswordRequest.Email);
            return _userControllerService.ForgetPasswordResponseGenerator(tokenResult.Status);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest resetPasswordRequest)
    {
        try
        {
            var resetResult = await _userService.ResetPasswordAsync(resetPasswordRequest.ToResetPasswordDto());
            return _userControllerService.ResetPasswordResponseGenerator(resetResult.Status);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }
}