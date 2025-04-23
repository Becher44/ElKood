using ElKood.Shared.Models.Errors;

namespace ElKood.Application.Common.Exceptions;

public class ValidationException(ErrorResponse errorResponse) : Exception
{
    public ErrorResponse ErrorResponse { get; private set; } = errorResponse;
}
