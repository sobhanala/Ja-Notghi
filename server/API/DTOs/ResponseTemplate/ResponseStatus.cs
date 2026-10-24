namespace API.DTOs.ResponseTemplate;

public enum ResponseStatus
{
    UnExpectedError = -1,
    
    //Login Status
    Login_Success = 1001,
    Login_UserLocked = 1002,
    Login_Failed = 1003,
    
    //Signup Status
    Signup_Success = 2001,
    Signup_InvalidDateOfBirth = 2002,
    Signup_InvalidPassword = 2003,
    Signup_InvalidEmail = 2004,
    Signup_InvalidUsername = 2005,
    Signup_InvalidFirstname = 2006,
    Signup_InvalidLastname = 2007,
    Signup_UserAlreadyExists = 2008,
    
    //ForgetPassword Status
    ForgetPassword_EmailSent = 3001,
    
    //ResetPassword Status
    ResetPassword_Success = 4001,
    ResetPassword_UserNotFound = 4002,
    ResetPassword_TokenExpired = 4003,
    
    //Follow Status
    Follow_Success = 5001,
    Follow_UserNotFound = 5002,
    Follow_FollowAlreadyExist = 5003,
    Follow_FollowYourself = 5004,
    
    //Unfollow Status
    Unfollow_Success = 6001,
    Unfollow_UserNotFound = 6002,
    Unfollow_FollowNotFound = 6003,
    Unfollow_UnfollowYourself = 6004,
    
    //GetFollowers Status
    GetFollowers_Success = 7001,
    GetFollowers_UserNotFound = 7002,
    
    //GetFollowings Status
    GetFollowings_Success = 8001,
    GetFollowings_UserNotFound = 8002,
}