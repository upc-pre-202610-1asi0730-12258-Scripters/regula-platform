using System.Text.Json.Serialization;

namespace Scripters.Regula.Platform.Iam.Domain.Model.Aggregates;

public partial class User(string username, string passwordHash)
{
    public User() : this(string.Empty, string.Empty)
    {
    }

    public int Id { get; }
    public string Username { get; private set; } = username;

    [JsonIgnore] public string PasswordHash { get; private set; } = passwordHash;
}
