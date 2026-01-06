using Ambev.DeveloperEvaluation.Domain.Queries;

namespace Ambev.DeveloperEvaluation.ORM.Queries;

public static class QueryablePaginationExtensions
{
    public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, QueryParameters parameters)
    {
        var skip = (parameters.Page - 1) * parameters.Size;
        return query.Skip(skip).Take(parameters.Size);
    }
}
