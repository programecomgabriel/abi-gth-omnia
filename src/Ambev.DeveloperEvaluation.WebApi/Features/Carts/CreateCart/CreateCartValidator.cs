using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

/// <summary>
/// Validator for <see cref="CreateCartCommand"/> that defines validation rules for Cart creation command.
/// </summary>
public class CreateCartValidator : AbstractValidator<CreateCartCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCartValidator"/> with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">ProductId: Required</list>
    /// <list type="bullet">Quantity: Required, must be greater than zero</list>
    /// </remarks>
    public CreateCartValidator()
    {
        RuleFor(p => p.ProductId).NotEmpty();
        RuleFor(p => p.Quantity).GreaterThan(0);
    }
}
