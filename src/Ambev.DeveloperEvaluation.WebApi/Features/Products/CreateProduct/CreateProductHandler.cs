using Ambev.DeveloperEvaluation.Domain.Products;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

/// <summary>
/// Handler for processing <see cref="CreateProductCommand"/> requests.
/// </summary>
public class CreateProductHandler(
    IProductRepository productRepository,
    IMapper mapper) : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    /// <summary>
    /// Handles the CreateProductCommand request
    /// </summary>
    /// <param name="command">The CreateProduct command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created product details</returns>
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = mapper.Map<Product>(command);

        await productRepository.CreateAsync(product, cancellationToken);

        return mapper.Map<CreateProductResult>(product);
    }
}
