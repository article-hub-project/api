using MongoDB.Bson.Serialization.Attributes;

namespace PL.ViewModels.Auth
{
    public record UserViewModel(
        string Id,
        string Username,
        string Email,
        DateTime DateRegistered
        );
}
