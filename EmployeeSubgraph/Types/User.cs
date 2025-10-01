using EmployeeSubgraph.Services;
using HotChocolate.ApolloFederation.Resolvers;
using HotChocolate.ApolloFederation.Types;

namespace EmployeeSubgraph.Types
{
    [Key("id")]
    public class User
    {
        [ID]
        [Key]
        public string Id { get; }


        [ReferenceResolver]
        public static User GetUserById(string id)
        {
            // Provide default values for username and fullname
            return new User(id);
        }

        [GraphQLDescription("Employee details for this user.")]
        public async Task<Employee?> EmployeeDetails([Service] EmployeeService employeeService)
        {
            return (await employeeService.GetAllAsync())
                .FirstOrDefault(e => e.UserId == this.Id);
        }

        public User(string id)
        {
            Id = id;
        }
    }
}
