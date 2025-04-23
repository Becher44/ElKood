using System.Diagnostics.CodeAnalysis;
using ElKood.Domain.Constants;

namespace ElKood.Application.Common.Exceptions;

[ExcludeFromCodeCoverage]
public static class ProgramException
{
    public static UserFriendlyException AppsettingNotSetException()
     => new(ErrorCode.Internal, ErrorMessage.AppConfigurationMessage, ErrorMessage.InternalError);
}
