namespace API.DTOs.ResponseTemplate;

public static class ResponseMessageMapper
{
    private static readonly Dictionary<ResponseStatus, string> ResponseMessages = new()
    {
        { ResponseStatus.UnExpectedError, API.ResponseMessages.UnExpectedError },
        
        // Login 
        { ResponseStatus.Login_Success, API.ResponseMessages.LoginSuccess },
        { ResponseStatus.Login_Failed, API.ResponseMessages.LoginFail },
        { ResponseStatus.Login_UserLocked, API.ResponseMessages.UserLocked },
        
        // Signin
        { ResponseStatus.Signup_Success, API.ResponseMessages.SignupSuccess },
        { ResponseStatus.Signup_InvalidEmail, API.ResponseMessages.InvalidEmail },
        { ResponseStatus.Signup_InvalidFirstname, API.ResponseMessages.InvalidFirstname },
        { ResponseStatus.Signup_InvalidLastname, API.ResponseMessages.InvalidLastname },
        { ResponseStatus.Signup_InvalidPassword, API.ResponseMessages.InvalidPassword },
        { ResponseStatus.Signup_InvalidDateOfBirth, API.ResponseMessages.InvalidDateOfBirth },
        { ResponseStatus.Signup_InvalidUsername, API.ResponseMessages.InvalidUsername },
        { ResponseStatus.Signup_UserAlreadyExists, API.ResponseMessages.UserAlreadyExists },
        
        // Forget Password
        { ResponseStatus.ForgetPassword_EmailSent, API.ResponseMessages.EmailSent },
        
        // Reset Password
        { ResponseStatus.ResetPassword_Success, API.ResponseMessages.PasswordResetSuccess },
        { ResponseStatus.ResetPassword_UserNotFound, API.ResponseMessages.UserNotFound },
        { ResponseStatus.ResetPassword_TokenExpired, API.ResponseMessages.TokenExpired },
        
        // Follow
        { ResponseStatus.Follow_Success, API.ResponseMessages.FollowSuccess },
        { ResponseStatus.Follow_UserNotFound, API.ResponseMessages.UserNotFound },
        { ResponseStatus.Follow_FollowAlreadyExist, API.ResponseMessages.FollowAlreadyExist },
        { ResponseStatus.Follow_FollowYourself, API.ResponseMessages.FollowYourself },
        
        // Unfollow
        { ResponseStatus.Unfollow_Success, API.ResponseMessages.UnfollowSuccess },
        { ResponseStatus.Unfollow_UserNotFound, API.ResponseMessages.UserNotFound },
        { ResponseStatus.Unfollow_FollowNotFound, API.ResponseMessages.FollowNotFound },
        { ResponseStatus.Unfollow_UnfollowYourself, API.ResponseMessages.UnfollowYourself },

        // GetFollowers
        { ResponseStatus.GetFollowers_Success, API.ResponseMessages.GetFollowersSuccess },
        { ResponseStatus.GetFollowers_UserNotFound, API.ResponseMessages.UserNotFound },
        
        // GetFollowings
        { ResponseStatus.GetFollowings_Success, API.ResponseMessages.GetFollowingsSuccess },
        { ResponseStatus.GetFollowings_UserNotFound, API.ResponseMessages.UserNotFound }
    };
    
    public static string GetMessage(this ResponseStatus status)
    {
        return ResponseMessages[status];
    }
}