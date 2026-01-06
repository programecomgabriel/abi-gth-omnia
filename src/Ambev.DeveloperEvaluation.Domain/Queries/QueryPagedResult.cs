namespace Ambev.DeveloperEvaluation.Domain.Queries;

/// <summary>
/// Represents the result from pagination query.
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class QueryPagedResult<T>
{
    /// <summary>
    /// Items of pagination.
    /// </summary>
    public IReadOnlyList<T> Items { get; init; } = [];

    /// <summary>
    /// Total items of pagination.
    /// </summary>
    public int TotalItems { get; init; }

    /// <summary>
    /// Selected page number.
    /// </summary>
    public int Page { get; init; }

    /// <summary>
    /// Selected page size.
    /// </summary>
    public int Size { get; init; }

    /// <summary>
    /// Total pages of que query.
    /// </summary>
    public int TotalPages => (int)Math.Ceiling((double)TotalItems / Size);
}
