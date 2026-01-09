using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.ChangeQuantityItemCart;

/// <summary>
/// Validator for <see cref="ChangeQuantityItemCartCommand"/> that defines validation rules for Cart creation command.
/// </summary>
public class ChangeQuantityItemCartValidator : AbstractValidator<ChangeQuantityItemCartCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ChangeQuantityItemCartValidator"/> with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">Id: Required</list>
    /// <list type="bullet">ProductId: Required</list>
    /// <list type="bullet">Quantity: Required, must be greater than zero</list>
    /// </remarks>
    public ChangeQuantityItemCartValidator()
    {
        RuleFor(p => p.Id).NotEmpty();
        RuleFor(p => p.ProductId).NotEmpty();
        RuleFor(p => p.Quantity).GreaterThan(0);
    }
}
