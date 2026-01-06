using Ambev.DeveloperEvaluation.Domain.Products;
using Ambev.DeveloperEvaluation.Domain.Queries;
using MediatR;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProducts;

/// <summary>
/// Handler for processing <see cref="GetProductsCommand"/> requests.
/// </summary>
public class GetProductsHandler(IProductRepository productRepository) : IRequestHandler<GetProductsCommand, QueryPagedResult<GetProductsResult>>
{
    /// <summary>
    /// Handles the GetProductsCommand request
    /// </summary>
    /// <param name="command">The GetProducts command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The all products found by command</returns>
    public async Task<QueryPagedResult<GetProductsResult>> Handle(GetProductsCommand request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetAllAsync(request.QueryParameters, cancellationToken);

        return new QueryPagedResult<GetProductsResult>
        {
            Items = [.. products.Items.Select(product => new GetProductsResult
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                Category = product.Category,
                Price = product.Price,
                Image = product.Image,
                Rating = product.Rating
            })],
            TotalItems = products.TotalItems,
            Page = products.Page,
            Size = products.Size
        };
    }
}
