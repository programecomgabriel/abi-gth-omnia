using Ambev.DeveloperEvaluation.Domain.Products;
using Ambev.DeveloperEvaluation.Domain.Queries;
using Ambev.DeveloperEvaluation.WebApi.Contracts.Products;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProducts;

/// <summary>
/// Profile for mapping between User entity and GetUserResponse
/// </summary>
public class GetProductsProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetUser operation
    /// </summary>
    public GetProductsProfile()
    {
        CreateMap<Product, GetProductsResult>();
        CreateMap<QueryPagedResult<Product>, QueryPagedResult<GetProductsResult>>();

        CreateMap<GetProductsResult, GetProductsResponse>();
        CreateMap<QueryPagedResult<GetProductsResult>, QueryPagedResult<GetProductsResponse>>();
    }
}
