using Ambev.DeveloperEvaluation.Domain.Products;
using Ambev.DeveloperEvaluation.WebApi.Contracts.Products;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

/// <summary>
/// Profile for mapping between
/// <see cref="CreateProductCommand"/> entity and <see cref="Product"/>. 
/// <see cref="Product"/> entity and <see cref="CreateProductResult"/>.
/// </summary>
public class CreateProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateProduct operation
    /// </summary>
    public CreateProductProfile()
    {
        CreateMap<CreateProductRequest, CreateProductCommand>();
        CreateMap<CreateProductCommand, Product>();
        CreateMap<Product, CreateProductResult>();
        CreateMap<CreateProductResult, CreateProductResponse>();
    }
}
