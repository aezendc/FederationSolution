using UserSubgraph.Services;

namespace UserSubgraph.Types
{
    public class Mutation
    {
        [GraphQLDescription("Create a new user.")]
        public async Task<User> CreateUser(
            string username,
            [Service] UserService userService)
        {
            // Pass empty id placeholder; service will ignore for identity
            return await userService.AddUserAsync(string.Empty, username);
        }

        [GraphQLDescription("Edit an existing user.")]
        public async Task<User?> EditUser(
            string id,
            string? username,
            [Service] UserService userService)
        {
            return await userService.EditUserAsync(id, username);
        }

        [GraphQLDescription("Delete a user by ID.")]
        public async Task<bool> DeleteUser(
            string id,
            [Service] UserService userService)
        {
            return await userService.DeleteUserAsync(id);
        }
    }
}
