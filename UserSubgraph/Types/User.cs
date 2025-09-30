using HotChocolate.ApolloFederation.Resolvers;
using HotChocolate.ApolloFederation.Types;

namespace UserSubgraph.Types
{
    [GraphQLDescription("Represents an application user.")]
    public class User
    {
        [GraphQLDescription("The unique identifier of the user.")]
        [ID]
        [Key]
        public string Id { get; }

        [GraphQLDescription("The username for login.")]
        public string Username { get; set; }

        [GraphQLDescription("The full display name of the user.")]
        public string FullName { get; set; }

        // This is how federation can rehydrate the entity
        [ReferenceResolver]
        public static User GetUserById(string id, string? username, string? fullName)
        {
            return new User(id, username ?? "unknown", fullName ?? "unknown");
        }

        public User(string id, string username, string fullName)
        {
            Id = id;
            Username = username;
            FullName = fullName;
        }
    }

}
