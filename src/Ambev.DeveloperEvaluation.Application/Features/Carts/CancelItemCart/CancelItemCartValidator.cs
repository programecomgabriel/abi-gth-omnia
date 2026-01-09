using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Features.Carts.CancelItemCart;

/// <summary>
/// Validator for <see cref="CancelItemCartCommand"/> that defines validation rules for Cart creation command.
/// </summary>
public class CancelItemCartValidator : AbstractValidator<CancelItemCartCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CancelItemCartValidator"/> with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">Id: Required</list>
    /// <list type="bullet">ProductId: Required</list>
    /// </remarks>
    public CancelItemCartValidator()
    {
        RuleFor(p => p.Id).NotEmpty();
        RuleFor(p => p.ProductId).NotEmpty();
    }
}
