using UserSubgraph.Services;

namespace UserSubgraph.Types
{
    public class Query
    {
        [GraphQLName("getUsers")]
        [GraphQLDescription("Returns a list of all registered users.")]
        public async Task<List<User>> GetUsers([Service] UserService userService)
        {
            return await userService.GetAllUsersAsync();
        }

        [GraphQLName("getUser")]
        [GraphQLDescription("Retrieves a specific user by their unique identifier.")]
        public async Task<User?> GetUser([ID] string id, [Service] UserService userService)
        {
            return await userService.GetUserByIdAsync(id);
        }
    }
}
