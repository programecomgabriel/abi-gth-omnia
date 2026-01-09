using Ambev.DeveloperEvaluation.Domain.Products;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Features.Products.CreateProduct;

/// <summary>
/// Handler for processing <see cref="CreateProductCommand"/> requests.
/// </summary>
public class CreateProductHandler(IProductRepository productRepository) : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    /// <summary>
    /// Handles the CreateProductCommand request
    /// </summary>
    /// <param name="command">The CreateProduct command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created product details</returns>
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Title = command.Title,
            Price = command.Price,
            Description = command.Description,
            Category = command.Category,
            Image = command.Image,
            Quantity = command.Quantity,
            Rating = command.Rating
        };

        await productRepository.CreateAsync(product, cancellationToken);

        return new CreateProductResult
        {
            Id = product.Id,
            Title = product.Title,
            Description = product.Description,
            Category = product.Category,
            Price = product.Price,
            Image = product.Image,
            Rating = product.Rating
        };
    }
}
