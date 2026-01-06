using Ambev.DeveloperEvaluation.Domain.Queries;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Queries;

public static class QueryablePagingExtensions
{
    public static async Task<QueryPagedResult<T>> ToPagedResultAsync<T>(this IQueryable<T> source, QueryParameters parameters, CancellationToken cancellationToken = default)
    {
        var filteredQuery = source.ApplyFiltering(parameters);

        var totalItems = await filteredQuery.CountAsync(cancellationToken);

        var items = await filteredQuery
            .ApplyOrdering(parameters)
            .ApplyPagination(parameters)
            .ToListAsync(cancellationToken);

        return new QueryPagedResult<T>
        {
            Items = items,
            TotalItems = totalItems,
            Page = parameters.Page,
            Size = parameters.Size
        };
    }
}
