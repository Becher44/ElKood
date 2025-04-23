using System.Diagnostics.CodeAnalysis;
using ElKood.Domain.Constants;

namespace ElKood.Application.Common.Exceptions;

[ExcludeFromCodeCoverage]
public static class AuthIdentityException
{
    public static UserFriendlyException ThrowTokenNotExist()
     => throw new UserFriendlyException(ErrorCode.NotFound, AuthIdentityErrorMessage.TokenNotExistMessage, AuthIdentityErrorMessage.TokenNotExistMessage);

    public static UserFriendlyException ThrowTokenNotActive()
        => throw new UserFriendlyException(ErrorCode.BadRequest, AuthIdentityErrorMessage.TokenNotActiveMessage, AuthIdentityErrorMessage.TokenNotActiveMessage);

    public static UserFriendlyException ThrowAccountDoesNotExist()
        => throw new UserFriendlyException(ErrorCode.NotFound, AuthIdentityErrorMessage.AccountDoesNotExistMessage, AuthIdentityErrorMessage.AccountDoesNotExistMessage);

    public static UserFriendlyException ThrowLoginUnsuccessful()
        => throw new UserFriendlyException(ErrorCode.BadRequest, AuthIdentityErrorMessage.LoginUnsuccessfulMessage, AuthIdentityErrorMessage.LoginUnsuccessfulMessage);

    public static UserFriendlyException ThrowUsernameAvailable()
        => throw new UserFriendlyException(ErrorCode.NotFound, AuthIdentityErrorMessage.UsernameAvailableMessage, AuthIdentityErrorMessage.UsernameAvailableMessage);

    public static UserFriendlyException ThrowEmailAvailable()
        => throw new UserFriendlyException(ErrorCode.NotFound, AuthIdentityErrorMessage.EmailAvailableMessage, AuthIdentityErrorMessage.EmailAvailableMessage);

    public static UserFriendlyException ThrowRegisterUnsuccessful(string errors)
        => throw new UserFriendlyException(ErrorCode.BadRequest, AuthIdentityErrorMessage.RegisterUnsuccessfulMessage, errors);

    public static UserFriendlyException ThrowUserNotExist()
        => throw new UserFriendlyException(ErrorCode.NotFound, AuthIdentityErrorMessage.UserNotExistMessage, AuthIdentityErrorMessage.UserNotExistMessage);

    public static UserFriendlyException ThrowUpdateUnsuccessful(string errors)
        => throw new UserFriendlyException(ErrorCode.BadRequest, AuthIdentityErrorMessage.UpdateUnsuccessfulMessage, errors);

    public static UserFriendlyException ThrowDeleteUnsuccessful()
        => throw new UserFriendlyException(ErrorCode.BadRequest, AuthIdentityErrorMessage.DeleteUnsuccessfulMessage, AuthIdentityErrorMessage.DeleteUnsuccessfulMessage);

    public static UserFriendlyException ThrowEmailRequired()
        => throw new UserFriendlyException(ErrorCode.NotFound, AuthIdentityErrorMessage.EmailRequiredMessage, AuthIdentityErrorMessage.EmailRequiredMessage);

    public static UserFriendlyException ThrowUserNotFound()
        => throw new UserFriendlyException(ErrorCode.NotFound, AuthIdentityErrorMessage.UserNotFoundMessage, AuthIdentityErrorMessage.UserNotFoundMessage);

}
