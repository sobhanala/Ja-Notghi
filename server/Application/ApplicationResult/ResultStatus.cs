namespace Application.ApplicationResult;

public enum ResultStatus
{
    Succeeded,
    UserNotFound,

    // User Service Result Status
    
    // Login Specific 
    UserLockedOut,
    WrongPassword,
    
    // Reset Password Specific
    InvalidToken,
    
    // Signup Specific
    InvalidDateOfBirth,
    InvalidPassword,
    InvalidEmail,
    InvalidUsername,
    InvalidFirstname,
    InvalidLastname,
    UserAlreadyExists,
    
    // Follow Service Result Status
    FollowAlreadyExist,
    FollowYourself,
    // Unfollow Specific
    FollowNotFound,
    UnfollowYourself
    
}