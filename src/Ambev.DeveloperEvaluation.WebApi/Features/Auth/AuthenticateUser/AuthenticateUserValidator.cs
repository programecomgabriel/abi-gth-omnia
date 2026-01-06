using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Auth.AuthenticateUser
{
    /// <summary>
    /// Validator for AuthenticateUserCommand
    /// </summary>
    public class AuthenticateUserValidator : AbstractValidator<AuthenticateUserCommand>
    {
        /// <summary>
        /// Initializes validation rules for AuthenticateUserCommand
        /// </summary>
        public AuthenticateUserValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6);
        }
    }
}
