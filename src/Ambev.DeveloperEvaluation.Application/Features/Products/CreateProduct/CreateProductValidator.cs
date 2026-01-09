using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Features.Products.CreateProduct;

/// <summary>
/// Validator for <see cref="CreateProductCommand"/> that defines validation rules for product creation command.
/// </summary>
public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateProductValidator"/> with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">Title: Required</list>
    /// <list type="bullet">Description: Required</list>
    /// <list type="bullet">Category name: Required</list>
    /// <list type="bullet">Price: Required, must be greater than zero</list>
    /// <list type="bullet">Image: Required</list>
    /// <list type="bullet">Rating: Required, must be not null</list>
    /// </remarks>
    public CreateProductValidator()
    {
        RuleFor(p => p.Title).NotEmpty();
        RuleFor(p => p.Description).NotEmpty();
        RuleFor(p => p.Category).NotEmpty();
        RuleFor(p => p.Price).GreaterThan(0);
        RuleFor(p => p.Image).NotEmpty();
        RuleFor(p => p.Rating).NotNull();

        When(p => p.Rating is not null, () =>
        {
            RuleFor(p => p.Rating.Rate).GreaterThanOrEqualTo(0);
            RuleFor(p => p.Rating.Count).GreaterThanOrEqualTo(0);
        });
    }
}
