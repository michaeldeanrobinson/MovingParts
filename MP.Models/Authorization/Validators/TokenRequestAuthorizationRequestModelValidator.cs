using FluentValidation;
using MP.Models.Authorization.Models;

namespace MP.Models.Authorization.Validators
{
    public class TokenRequestAuthorizationRequestModelValidator : AbstractValidator<AuthorizationTokenRequestModel>
    {
        public TokenRequestAuthorizationRequestModelValidator()
        {
            RuleFor(login => login.Username).NotEmpty();
            RuleFor(login => login.Password).NotEmpty();
        }
    }
}
