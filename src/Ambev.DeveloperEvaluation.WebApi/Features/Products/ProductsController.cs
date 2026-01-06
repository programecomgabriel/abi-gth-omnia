using Ambev.DeveloperEvaluation.Domain.Queries;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Contracts.Products;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProducts;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products;

/// <summary>
/// Controller for managing product operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProductsController(IMediator mediator, IMapper mapper) : BaseController
{
    /// <summary>
    /// Retrieves products by querying parameters
    /// </summary>
    /// <param name="query">Dictionary of query string parameters used to filter, sort, and paginate products.
    /// Some supported keys (all values are strings):
    /// - "id": Product identifier (GUID or string) — filters by exact id.
    /// - "name": Partial or full product name to match.
    /// - "category": Product category name.
    /// - "minPrice": Minimum price (numeric string).
    /// - "maxPrice": Maximum price (numeric string).
    /// - "page": Page number (integer string, default 1).
    /// - "pageSize": Items per page (integer string).
    /// - "sortBy": Field name to sort by (e.g., "name", "price").
    /// - "sortOrder": "asc" or "desc" (default "asc").</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The all products found by query</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseWithData<QueryPagedResult<GetProductsResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProducts([FromQuery] Dictionary<string, string> query, CancellationToken cancellationToken)
    {
        var command = new GetProductsCommand(query);
        var response = await mediator.Send(command, cancellationToken);

        return Ok(mapper.Map<QueryPagedResult<GetProductsResponse>>(response));
    }

    /// <summary>
    /// Creates a new product
    /// </summary>
    /// <param name="request">The product creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created product details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateProductResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
    {
        var command = mapper.Map<CreateProductCommand>(request);
        var response = await mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateProductResponse>
        {
            Success = true,
            Message = "Product created successfully",
            Data = mapper.Map<CreateProductResponse>(response)
        });
    }
}
