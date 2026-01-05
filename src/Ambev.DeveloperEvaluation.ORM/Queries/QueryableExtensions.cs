using Ambev.DeveloperEvaluation.Domain.Queries;

namespace Ambev.DeveloperEvaluation.ORM.Queries;

public static class QueryableExtensions
{
    public static IQueryable<T> ApplyQuery<T>(this IQueryable<T> query, QueryParameters parameters)
    {
        return query
            .ApplyFiltering(parameters)
            .ApplyOrdering(parameters)
            .ApplyPagination(parameters);
    }
}
