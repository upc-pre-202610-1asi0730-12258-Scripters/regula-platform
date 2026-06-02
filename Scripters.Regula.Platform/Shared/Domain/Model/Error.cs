namespace Scripters.Regula.Platform.Shared.Domain.Model;

public record Error(string Code, string Message)
{
    public static readonly Error None =  new(string.Empty, string.Empty);
    public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null.");
}