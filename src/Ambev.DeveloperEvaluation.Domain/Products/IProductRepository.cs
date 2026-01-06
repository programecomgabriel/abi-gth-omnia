using Ambev.DeveloperEvaluation.Domain.Queries;

namespace Ambev.DeveloperEvaluation.Domain.Products;

/// <summary>
/// Repository interface for Product entity operations.
/// </summary>
public interface IProductRepository
{
    /// <summary>
    /// Retrieves products by query.
    /// </summary>
    /// <param name="query">The dictionary with query specifications</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The all products found based on query, zero lenght list otherwise</returns>
    Task<QueryPagedResult<Product>> GetAllAsync(QueryParameters parameters, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a product by id.
    /// </summary>
    /// <param name="id">The unique identifier of the product</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product if found, null otherwise</returns>
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <param name="product">The product to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product itself</returns>
    Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a product.
    /// </summary>
    /// <param name="id">The unique identifier of the product to delete</param>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
