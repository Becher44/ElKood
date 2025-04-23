using ElKood.Shared.Models.User;
using FluentValidation;

namespace ElKood.Web.Validations;
public class UserSignInRequestValidation : AbstractValidator<UserSignInRequest>
{
    public UserSignInRequestValidation()
    {
        RuleFor(x => x.UserName).NotEmpty().MaximumLength(100);
    }
}
