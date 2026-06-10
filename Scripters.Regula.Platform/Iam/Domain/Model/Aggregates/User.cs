using System.Text.Json.Serialization;

namespace Scripters.Regula.Platform.Iam.Domain.Model.Aggregates;

public partial class User(string username, string passwordHash)
{
    public User() : this(string.Empty, string.Empty)
    {
    }

    public int Id { get; private set; }
    public string Username { get; private set; } = username;

    [JsonIgnore] public string PasswordHash { get; private set; } = passwordHash;
    
    public User UpdateUsername(string username)
    {
        Username = username;
        return this;
    }
    
    public User UpdatePasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
        return this;
    }
}
