namespace ElKood.Domain.Constants;

public static class AuthIdentityErrorMessage
{
    public const string TokenNotExistMessage = "The specified token does not exist.";
    public const string TokenNotActiveMessage = "The specified token is not active.";
    public const string AccountDoesNotExistMessage = "The account does not exist.";
    public const string LoginUnsuccessfulMessage = "Login was unsuccessful. Check you password again!";
    public const string RefreshTokenUnsuccessfulMessage = "Refresh token was unsuccessful.";
    public const string UsernameAvailableMessage = "The username is available.";
    public const string EmailAvailableMessage = "The email address is available.";
    public const string RegisterUnsuccessfulMessage = "Registration was unsuccessful.";
    public const string UserNotExistMessage = "The user does not exist.";
    public const string UpdateUnsuccessfulMessage = "The update operation was unsuccessful.";
    public const string DeleteUnsuccessfulMessage = "The delete operation was unsuccessful.";
    public const string EmailRequiredMessage = "An email address is required.";
    public const string UserNotFoundMessage = "The user was not found.";
    public const string EmailAndPasswordNotNullMessage = "Email and password must not be null.";
}
