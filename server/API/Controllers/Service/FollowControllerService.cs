using API.DTOs.ResponseDtos;
using API.DTOs.ResponseTemplate;
using API.Mappers;
using Application.ApplicationResult;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Utils;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Service;

public class FollowControllerService : ControllerBase
{
    public IActionResult GenerateFollowResponse(ResultStatus status)
    {
        switch (status)
        {
            case ResultStatus.Succeeded:
                return Ok(new Response
                {
                    StatusCode = ResponseStatus.Follow_Success,
                    Message = ResponseStatus.Follow_Success.GetMessage(),
                    Data = null
                });
            case ResultStatus.UserNotFound:
                return BadRequest(new Response
                {
                    StatusCode = ResponseStatus.Follow_UserNotFound,
                    Message = ResponseStatus.Follow_UserNotFound.GetMessage(),
                    Data = null
                });
            case ResultStatus.FollowAlreadyExist:
                return BadRequest(new Response
                {
                    StatusCode = ResponseStatus.Follow_FollowAlreadyExist,
                    Message = ResponseStatus.Follow_FollowAlreadyExist.GetMessage(),
                    Data = null
                });
            case ResultStatus.FollowYourself:
                return BadRequest(new Response
                {
                    StatusCode = ResponseStatus.Follow_FollowYourself,
                    Message = ResponseStatus.Follow_FollowYourself.GetMessage(),
                    Data = null
                });
            default:
                throw new UnexpectedException(nameof(GenerateFollowResponse));
        }
    }
    
    public IActionResult GenerateUnfollowResponse(ResultStatus status)
    {
        switch (status)
        {
            case ResultStatus.Succeeded:
                return Ok(new Response
                {
                    StatusCode = ResponseStatus.Unfollow_Success,
                    Message = ResponseStatus.Unfollow_Success.GetMessage(),
                    Data = null
                });
            case ResultStatus.UserNotFound:
                return BadRequest(new Response
                {
                    StatusCode = ResponseStatus.Unfollow_UserNotFound,
                    Message = ResponseStatus.Unfollow_UserNotFound.GetMessage(),
                    Data = null
                });
            case ResultStatus.FollowNotFound:
                return BadRequest(new Response
                {
                    StatusCode = ResponseStatus.Unfollow_FollowNotFound,
                    Message = ResponseStatus.Unfollow_FollowNotFound.GetMessage(),
                    Data = null
                });
            case ResultStatus.UnfollowYourself:
                return BadRequest(new Response
                {
                    StatusCode = ResponseStatus.Unfollow_UnfollowYourself,
                    Message = ResponseStatus.Unfollow_UnfollowYourself.GetMessage(),
                    Data = null
                });
            default:
                throw new UnexpectedException(nameof(GenerateUnfollowResponse));
        }
    }
    
    public IActionResult GenerateGetFollowersResponse(ResultStatus status, PaginatedList<User>? followers)
    {
        switch (status)
        {
            case ResultStatus.Succeeded:
                return Ok(new Response
                {
                    StatusCode = ResponseStatus.GetFollowers_Success,
                    Message = ResponseStatus.GetFollowers_Success.GetMessage(),
                    Data = new PaginatedList<UserFollowDto>
                    {
                        Items = followers.Items.Select(u => u.ToUserFollowDto()),
                        PageNumber = followers.PageNumber,
                        PageSize = followers.PageSize,
                        TotalCount = followers.TotalCount
                    }
                });
            case ResultStatus.UserNotFound:
                return BadRequest(new Response
                {
                    StatusCode = ResponseStatus.GetFollowers_UserNotFound,
                    Message = ResponseStatus.GetFollowers_UserNotFound.GetMessage(),
                    Data = null
                });
            default:
                throw new UnexpectedException(nameof(GenerateGetFollowersResponse));
        }
    }
    
    public IActionResult GenerateGetFollowingsResponse(ResultStatus status, PaginatedList<User>? followings)
    {
        switch (status)
        {
            case ResultStatus.Succeeded:
                return Ok(new Response
                {
                    StatusCode = ResponseStatus.GetFollowings_Success,
                    Message = ResponseStatus.GetFollowings_Success.GetMessage(),
                    Data = new PaginatedList<UserFollowDto>
                    {
                        Items = followings.Items.Select(u => u.ToUserFollowDto()),
                        PageNumber = followings.PageNumber,
                        PageSize = followings.PageSize,
                        TotalCount = followings.TotalCount
                    }
                });
            case ResultStatus.UserNotFound:
                return BadRequest(new Response
                {
                    StatusCode = ResponseStatus.GetFollowings_UserNotFound,
                    Message = ResponseStatus.GetFollowings_UserNotFound.GetMessage(),
                    Data = null
                });
            default:
                throw new UnexpectedException(nameof(GenerateGetFollowingsResponse));
        }
    }
}