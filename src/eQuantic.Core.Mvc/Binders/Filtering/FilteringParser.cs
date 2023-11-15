using System.Text.RegularExpressions;
using eQuantic.Linq.Filter;

namespace eQuantic.Core.Mvc.Binders.Filtering;

/// <summary>
/// The filtering parser class
/// </summary>
public static class FilteringParser
{
    /// <summary>
    /// The list
    /// </summary>
    private static readonly Type[] ValidListTypes = new Type[] {
        typeof(IEnumerable<>),
        typeof(ICollection<>),
        typeof(IList<>),
        typeof(List<>)
    };

    /// <summary>
    /// Parses the values
    /// </summary>
    /// <param name="values">The values</param>
    /// <exception cref="ArgumentException"></exception>
    /// <returns>An enumerable of i filtering</returns>
    public static IEnumerable<IFiltering> Parse(string values)
    {
        if (string.IsNullOrEmpty(values)) 
            throw new ArgumentException(null, nameof(values));

        var matches = Regex.Matches(values, Linq.Filter.Filtering.ArgsRegex);
        return !matches.Any() ? 
            new List<IFiltering> { CompositeFiltering.ParseComposite(values) } : 
            matches.Select(m => CompositeFiltering.ParseComposite(m.Value.Trim()));
    }

    /// <summary>
    /// Describes whether is valid list type
    /// </summary>
    /// <param name="type">The type</param>
    /// <returns>The bool</returns>
    internal static bool IsValidListType(Type type)
    {
        return type.IsGenericType && ValidListTypes.Any(t => t.IsAssignableFrom(type.GetGenericTypeDefinition()));
    }
}