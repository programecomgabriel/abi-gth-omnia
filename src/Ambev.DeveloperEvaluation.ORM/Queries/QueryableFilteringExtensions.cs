using Ambev.DeveloperEvaluation.Domain.Queries;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.ORM.Queries;

public static class QueryableFilteringExtensions
{
    public static IQueryable<T> ApplyFiltering<T>(this IQueryable<T> query, QueryParameters parameters)
    {
        foreach (var (key, value) in parameters.Filters)
        {
            if (key.StartsWith("_min", StringComparison.OrdinalIgnoreCase))
            {
                var field = key[4..];
                query = query.Where(BuildComparisonExpression<T>(field, value, ComparisonType.GreaterOrEqual));
            }
            else if (key.StartsWith("_max", StringComparison.OrdinalIgnoreCase))
            {
                var field = key[4..];
                query = query.Where(BuildComparisonExpression<T>(field, value, ComparisonType.LessOrEqual));
            }
            else
            {
                query = query.Where(BuildEqualsExpression<T>(key, value));
            }
        }

        return query;
    }

    private static Expression<Func<T, bool>> BuildEqualsExpression<T>(string propertyName, string value)
    {
        var parameter = Expression.Parameter(typeof(T), "e");
        var property = GetPropertyExpression<T>(parameter, propertyName);

        if (property.Type == typeof(string))
        {
            if (value.Contains('*'))
            {
                var pattern = value.Replace('*', '%');
                var likeMethod = typeof(NpgsqlDbFunctionsExtensions)
                    .GetMethod(
                        nameof(NpgsqlDbFunctionsExtensions.ILike),
                        [typeof(DbFunctions), typeof(string), typeof(string)]
                    )!;

                var functions = Expression.Property(null, typeof(EF), nameof(EF.Functions));
                var call = Expression.Call(likeMethod, functions, property, Expression.Constant(pattern));

                return Expression.Lambda<Func<T, bool>>(call, parameter);
            }

            var equals = Expression.Equal(property, Expression.Constant(value));
            return Expression.Lambda<Func<T, bool>>(equals, parameter);
        }

        var convertedValue = Convert.ChangeType(value, property.Type);
        var constant = Expression.Constant(convertedValue);
        var comparison = Expression.Equal(property, constant);

        return Expression.Lambda<Func<T, bool>>(comparison, parameter);
    }

    private static Expression<Func<T, bool>> BuildComparisonExpression<T>(
        string propertyName,
        string value,
        ComparisonType comparisonType)
    {
        var parameter = Expression.Parameter(typeof(T), "e");
        var property = GetPropertyExpression<T>(parameter, propertyName);

        var convertedValue = Convert.ChangeType(value, Nullable.GetUnderlyingType(property.Type) ?? property.Type);
        var constant = Expression.Constant(convertedValue);

        Expression comparison = comparisonType switch
        {
            ComparisonType.GreaterOrEqual => Expression.GreaterThanOrEqual(property, constant),
            ComparisonType.LessOrEqual => Expression.LessThanOrEqual(property, constant),
            _ => throw new NotSupportedException()
        };

        return Expression.Lambda<Func<T, bool>>(comparison, parameter);
    }

    private static MemberExpression GetPropertyExpression<T>(ParameterExpression parameter, string propertyName)
    {
        var property = typeof(T)
            .GetProperties()
            .FirstOrDefault(p => p.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase)) ??
                                 throw new InvalidOperationException($"Property '{propertyName}' not found on '{typeof(T).Name}'");

        return Expression.Property(parameter, property);
    }

    private enum ComparisonType
    {
        GreaterOrEqual,
        LessOrEqual
    }
}
