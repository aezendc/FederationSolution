using EmployeeSubgraph.Services;

namespace EmployeeSubgraph.Types;

public class Mutation
{
    [GraphQLDescription("Create a new employee.")]
    public Task<Employee> CreateEmployee(int userId, string? firstName, string? lastName, [Service] EmployeeService svc)
        => svc.AddAsync(userId, firstName, lastName);
}
