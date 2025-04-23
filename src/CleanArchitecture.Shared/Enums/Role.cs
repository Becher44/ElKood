using System.Text.Json.Serialization;

namespace ElKood.Shared.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Role
{
    Admin,
    User
}
