using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Features.Products.GetProducts;

/// <summary>
/// Validator for GetProductsCommand
/// </summary>
public class GetProductsValidator : AbstractValidator<GetProductsCommand>
{
    /// <summary>
    /// Initializes validation rules for GetProductsCommand
    /// </summary>
    public GetProductsValidator()
    {
        RuleFor(x => x.QueryParameters.Size).LessThan(1000);
    }
}