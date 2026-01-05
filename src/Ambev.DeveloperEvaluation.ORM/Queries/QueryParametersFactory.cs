using Ambev.DeveloperEvaluation.Domain.Queries;

namespace Ambev.DeveloperEvaluation.ORM.Queries;

public static class QueryParametersFactory
{
    /// <summary>
    /// Factory method to create <see cref="QueryParameters"/> from a dictionary
    /// </summary>
    /// <param name="query">The dictionary with query specifications</param>
    /// <returns><see cref="QueryParameters"/></returns>
    public static QueryParameters FromQuery(IDictionary<string, string> query)
    {
        var parameters = new QueryParameters();

        foreach (var (key, value) in query)
        {
            if (string.IsNullOrWhiteSpace(value)) continue;

            switch (key)
            {
                case "_page":
                    parameters.Page = Math.Max(1, int.Parse(value));
                    break;

                case "_size":
                    parameters.Size = Math.Clamp(int.Parse(value), 1, 100);
                    break;

                case "_order":
                    parameters.Order = value;
                    break;

                default:
                    parameters.Filters[key] = value;
                    break;
            }
        }

        return parameters;
    }
}
