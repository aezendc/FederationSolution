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

        // This is how federation can rehydrate the entity
        [ReferenceResolver]
        public static User GetUserById(string id, string? username)
        {
            return new User(id, username ?? "unknown");
        }

        public User(string id, string username)
        {
            Id = id;
            Username = username;
        }
    }

}
