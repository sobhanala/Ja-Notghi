using System.Security.Claims;
using API.Controllers.Service;
using API.DTOs.Requests;
using API.DTOs.ResponseTemplate;
using Application.interfaces.ApplicationServices;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class FollowController : ControllerBase
{
    private readonly IFollowService _followService;
    private readonly ILogger<FollowController> _logger;
    private readonly FollowControllerService _followControllerService;
    public FollowController(IFollowService followService, ILogger<FollowController> logger, FollowControllerService followControllerService)
    {
        _followService = followService;
        _logger = logger;
        _followControllerService = followControllerService;
    }
    
    private IActionResult HandleException(Exception e)
    {
        _logger.LogCritical(e.ToString());
        return StatusCode(StatusCodes.Status500InternalServerError, new Response
        {
            StatusCode = ResponseStatus.UnExpectedError,
            Message = ResponseStatus.UnExpectedError.GetMessage(),
            Data = null
        });
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Follow([FromBody] FollowRequest followRequest)
    {
        try
        {
            var followerUsername = User.FindFirst(ClaimTypes.Name)?.Value;

            if (followerUsername == null)
                throw new UnexpectedException(nameof(Follow));
            
            var followResult = await _followService.FollowAsync(followerUsername, followRequest.FolloweeUsername);
            return _followControllerService.GenerateFollowResponse(followResult.Status);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Unfollow([FromBody] FollowRequest followRequest)
    {
        try
        {
            var followerUsername = User.FindFirst(ClaimTypes.Name)?.Value;

            if (followerUsername == null)
                throw new UnexpectedException(nameof(Unfollow));

            var followResult = await _followService.UnfollowAsync(followerUsername, followRequest.FolloweeUsername);
            return _followControllerService.GenerateUnfollowResponse(followResult.Status);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }
    
    [HttpGet("{username}")]
    public async Task<IActionResult> GetFollowers([FromRoute] string username, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var followersResult = await _followService.GetFollowersAsync(username, pageNumber, pageSize);
            return _followControllerService.GenerateGetFollowersResponse(followersResult.Status, followersResult.Value);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }
    
    [HttpGet("{username}")]
    public async Task<IActionResult> GetFollowings([FromRoute] string username, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var followingsResult = await _followService.GetFollowingsAsync(username, pageNumber, pageSize);
            return _followControllerService.GenerateGetFollowingsResponse(followingsResult.Status, followingsResult.Value);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }
}