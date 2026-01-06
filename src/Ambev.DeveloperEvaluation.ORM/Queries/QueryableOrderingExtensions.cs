using Ambev.DeveloperEvaluation.Domain.Queries;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.ORM.Queries;

public static class QueryableOrderingExtensions
{
    public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, QueryParameters parameters)
    {
        if (string.IsNullOrWhiteSpace(parameters.Order))
            return query;

        IOrderedQueryable<T>? orderedQuery = null;

        var orders = parameters.Order.Split(',', StringSplitOptions.RemoveEmptyEntries);

        foreach (var order in orders)
        {
            var parts = order.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var property = parts[0];
            var descending = parts.Length > 1 &&
                             parts[1].Equals("desc", StringComparison.OrdinalIgnoreCase);

            orderedQuery = ApplyOrder(query, orderedQuery, property, descending);
        }

        return orderedQuery ?? query;
    }

    private static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, IOrderedQueryable<T>? ordered, string propertyName, bool descending)
    {
        var parameter = Expression.Parameter(typeof(T), "e");
        var property = typeof(T)
            .GetProperties()
            .FirstOrDefault(p => p.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase)) ??
                                 throw new InvalidOperationException($"Property '{propertyName}' not found on '{typeof(T).Name}'");

        var propertyAccess = Expression.Property(parameter, property);
        var lambda = Expression.Lambda(propertyAccess, parameter);

        string methodName;

        if (ordered == null)
        {
            methodName = descending ? "OrderByDescending" : "OrderBy";
        }
        else
        {
            methodName = descending ? "ThenByDescending" : "ThenBy";
        }

        var method = typeof(Queryable)
            .GetMethods()
            .Single(m => m.Name == methodName && m.GetParameters().Length == 2)
            .MakeGenericMethod(typeof(T), property.PropertyType);

        return (IOrderedQueryable<T>)method.Invoke(null, [ordered ?? source, lambda])!;
    }
}
