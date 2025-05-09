using System.Diagnostics.CodeAnalysis;
using ElKood.Domain.Constants;

namespace ElKood.Application.Common.Exceptions;

[ExcludeFromCodeCoverage]
public static class TransactionException
{
    public static UserFriendlyException TransactionNotCommitException()
        => throw new UserFriendlyException(ErrorCode.Internal, ErrorMessage.TransactionNotCommit, ErrorMessage.TransactionNotCommit);

    public static UserFriendlyException TransactionNotExecuteException(Exception ex)
        => throw new UserFriendlyException(ErrorCode.Internal, ErrorMessage.TransactionNotExecute, ErrorMessage.TransactionNotExecute, ex);
}
