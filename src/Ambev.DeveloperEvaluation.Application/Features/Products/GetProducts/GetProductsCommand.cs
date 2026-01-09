using Ambev.DeveloperEvaluation.Domain.Queries;
using Ambev.DeveloperEvaluation.ORM.Queries;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Features.Products.GetProducts;

public class GetProductsCommand : IRequest<QueryPagedResult<GetProductsResult>>
{
    public GetProductsCommand(Dictionary<string, string> query)
    {
        QueryParameters = QueryParametersFactory.FromQuery(query);
    }

    public QueryParameters QueryParameters { get; set; }
}
