using HotChocolate.ApolloFederation.Types;
using HotChocolate.ApolloFederation.Resolvers;

namespace EmployeeSubgraph.Types;

[GraphQLDescription("Represents an employee record.")]
public class Employee
{
    [ID]
    [Key]
    public string Id { get; }
    public string UserId { get; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    [ReferenceResolver]
    public static Employee GetEmployeeById(string id, string? userId, string? firstName, string? lastName)
        => new(id, userId ?? "0", firstName, lastName);

    public Employee(string id, string userId, string? firstName, string? lastName)
    {
        Id = id;
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
    }
}
