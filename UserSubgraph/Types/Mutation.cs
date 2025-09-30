using UserSubgraph.Services;

namespace UserSubgraph.Types
{
    public class Mutation
    {
        [GraphQLDescription("Create a new user.")]
        public async Task<User> CreateUser(
            string username,
            string fullName,
            [Service] UserService userService)
        {
            // Pass empty id placeholder; service will ignore for identity
            return await userService.AddUserAsync(string.Empty, username, fullName);
        }
    }
}
