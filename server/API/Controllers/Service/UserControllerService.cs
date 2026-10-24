using API.DTOs.ResponseTemplate;
using API.Mappers;
using Application.ApplicationResult;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace API.Controllers.Service;

/// <summary>
/// This service class provides utility methods to generate standardized HTTP responses 
/// based on the result status of various operations such as login, signup, password reset, etc.
/// 
/// The class leverages switch-case constructs to handle different possible outcomes of these operations
/// and returns the appropriate IActionResult using methods inherited from ControllerBase, 
/// such as Ok(), BadRequest(), and StatusCode().
/// 
/// By centralizing the response generation logic here, we ensure consistency and reduce 
/// repetition across the UserController.
/// </summary>
public class UserControllerService : ControllerBase
{
    public IActionResult LoginResponseGenerator(ResultStatus status, object? token)
    {
        switch (status)
        {
            case ResultStatus.Succeeded:
            {
                return Ok(new Response
                {
                    StatusCode = ResponseStatus.Login_Success,
                    Message = ResponseStatus.Login_Success.GetMessage(),
                    Data = new { Token = new JwtSecurityTokenHandler().WriteToken((JwtSecurityToken)token) }
                });
            }
            case ResultStatus.UserNotFound or ResultStatus.WrongPassword:
                return BadRequest(new Response
                {
                    StatusCode = ResponseStatus.Login_Failed,
                    Message = ResponseStatus.Login_Failed.GetMessage(),
                    Data = null
                });
            case ResultStatus.UserLockedOut:
                return StatusCode(423, new Response
                {
                    StatusCode = ResponseStatus.Login_UserLocked,
                    Message = ResponseStatus.Login_UserLocked.GetMessage(),
                    Data = null
                });
            default:
                throw new UnexpectedException(nameof(LoginResponseGenerator));
        }
    }
    public IActionResult SignupResponseGenerator(ResultStatus status, object userToSignup)
    {
        switch (status)
        {
            case ResultStatus.Succeeded:
                return Ok(new Response
                {
                    StatusCode = ResponseStatus.Signup_Success,
                    Message = ResponseStatus.Signup_Success.GetMessage(),
                    Data = ((User)userToSignup).ToUserResponse()
                });
            case ResultStatus.InvalidEmail:
                return BadRequest(new Response
                {
                    StatusCode = ResponseStatus.Signup_InvalidEmail,
                    Message = ResponseStatus.Signup_InvalidEmail.GetMessage(),
                    Data = null
                });
            case ResultStatus.InvalidPassword:
                return BadRequest(new Response
                {
                    StatusCode = ResponseStatus.Signup_InvalidPassword,
                    Message = ResponseStatus.Signup_InvalidPassword.GetMessage(),
                    Data = null
                });
            case ResultStatus.InvalidFirstname:
                return BadRequest(new Response
                {
                    StatusCode = ResponseStatus.Signup_InvalidFirstname,
                    Message = ResponseStatus.Signup_InvalidFirstname.GetMessage(),
                    Data = null
                });

            case ResultStatus.InvalidLastname:
                return BadRequest(new Response
                {
                    StatusCode = ResponseStatus.Signup_InvalidLastname,
                    Message = ResponseStatus.Signup_InvalidLastname.GetMessage(),
                    Data = null
                });

            case ResultStatus.InvalidDateOfBirth:
                return BadRequest(new Response
                {
                    StatusCode = ResponseStatus.Signup_InvalidDateOfBirth,
                    Message = ResponseStatus.Signup_InvalidDateOfBirth.GetMessage(),
                    Data = null
                });

            case ResultStatus.InvalidUsername:
                return BadRequest(new Response
                {
                    StatusCode = ResponseStatus.Signup_InvalidUsername,
                    Message = ResponseStatus.Signup_InvalidUsername.GetMessage(),
                    Data = null
                });
            case ResultStatus.UserAlreadyExists:
                return BadRequest(new Response
                {
                    StatusCode = ResponseStatus.Signup_UserAlreadyExists,
                    Message = ResponseStatus.Signup_UserAlreadyExists.GetMessage(),
                    Data = null
                });
            default:
                throw new UnexpectedException(nameof(SignupResponseGenerator));
        }
    }
    public IActionResult ForgetPasswordResponseGenerator(ResultStatus status)
    {
        switch (status)
        {
            case ResultStatus.Succeeded:
                return Ok(new Response
                {
                    StatusCode = ResponseStatus.ForgetPassword_EmailSent,
                    Message = ResponseStatus.ForgetPassword_EmailSent.GetMessage(),
                    Data = null
                });

            case ResultStatus.UserNotFound:
            {
                return NotFound(new Response
                {
                    StatusCode = ResponseStatus.ForgetPassword_EmailSent,
                    Message = ResponseStatus.ForgetPassword_EmailSent.GetMessage(),
                    Data = null
                });
            }
            default:
                throw new UnexpectedException(nameof(ForgetPasswordResponseGenerator));
        }
    }
    public IActionResult ResetPasswordResponseGenerator(ResultStatus status)
    {
        switch (status)
        {
            case ResultStatus.Succeeded:
                return Ok(new Response
                {
                    StatusCode = ResponseStatus.ResetPassword_Success,
                    Message = ResponseStatus.ResetPassword_Success.GetMessage(),
                    Data = null
                });
            case ResultStatus.UserNotFound:
                return BadRequest(new Response
                {
                    StatusCode = ResponseStatus.ResetPassword_UserNotFound,
                    Message = ResponseStatus.ResetPassword_UserNotFound.GetMessage(),
                    Data = null
                });
            case ResultStatus.InvalidToken:
                return BadRequest(new Response
                {
                    StatusCode = ResponseStatus.ResetPassword_TokenExpired,
                    Message = ResponseStatus.ResetPassword_TokenExpired.GetMessage(),
                    Data = null
                });
            default:
                throw new UnexpectedException(nameof(ResetPasswordResponseGenerator));
        }
    }
}