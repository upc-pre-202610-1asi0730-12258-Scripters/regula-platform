using System.Text.RegularExpressions;

namespace Scripters.Regula.Platform.Shared.Infrastructure.Interfaces.AspNetCore.Configuration.Extensions;

/// <summary>
///     String utility extensions used by the ASP.NET Core infrastructure layer.
/// </summary>
public static partial class StringExtensions
{
    /// <summary>
    ///     Converts a PascalCase or camelCase string to kebab-case.
    /// </summary>
    /// <param name="text">The string to convert.</param>
    /// <returns>The kebab-case representation of <paramref name="text"/>.</returns>
    public static string ToKebabCase(this string text)
    {
        if (string.IsNullOrEmpty(text)) return text;

        return KebabCaseRegex().Replace(text, "-$1")
            .Trim()
            .ToLower();
    }

    [GeneratedRegex("(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])", RegexOptions.Compiled)]
    private static partial Regex KebabCaseRegex();
}