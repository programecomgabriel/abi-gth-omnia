namespace Ambev.DeveloperEvaluation.Domain.Queries;

/// <summary>
/// Represents query parameters commonly used for paging, sorting and filtering
/// when querying data from a repository or API endpoint.
/// </summary>
/// <remarks>
/// This class provides simple pagination controls via <see cref="Page"/> and
/// <see cref="Size"/>, a sorting expression via <see cref="Order"/>, and a
/// flexible set of additional filtering key/value pairs via <see cref="Filters"/>.
/// The <see cref="Filters"/> dictionary is case-insensitive for keys.
/// </remarks>
public sealed class QueryParameters
{
    /// <summary>
    /// Gets or sets the page number to retrieve. This is a 1-based index.
    /// Default is <c>1</c>.
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Gets or sets the number of items per page.
    /// Default is <c>10</c>.
    /// </summary>
    public int Size { get; set; } = 10;

    /// <summary>
    /// Gets or sets the ordering expression. Common formats include
    /// "Name" or "Name desc" to indicate descending order.
    /// </summary>
    public string? Order { get; set; }

    /// <summary>
    /// Gets the filters to apply to the query as key/value pairs.
    /// Keys are compared in a case-insensitive manner.
    /// </summary>
    /// <remarks>
    /// Example usage:
    /// <code>
    /// parameters.Filters["status"] = "active";
    /// parameters.Filters["category"] = "books";
    /// </code>
    /// </remarks>
    public IDictionary<string, string> Filters { get; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
}
