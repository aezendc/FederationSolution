using EmployeeSubgraph.Services;

namespace EmployeeSubgraph.Types;

public class Mutation
{
    [GraphQLDescription("Create a new employee.")]
    public async Task<Employee> CreateEmployee(
        int userId,
        string? firstName,
        string? lastName,
        [Service] EmployeeService svc)
    {
        return await svc.AddAsync(userId, firstName, lastName);
    }

    [GraphQLDescription("Edit an existing employee.")]
    public async Task<Employee?> EditEmployee(
        string id,
        string? firstName,
        string? lastName,
        [Service] EmployeeService svc)
    {
        return await svc.EditAsync(id, firstName, lastName);
    }

    [GraphQLDescription("Delete an employee by ID.")]
    public async Task<bool> DeleteEmployee(
        string id,
        [Service] EmployeeService svc)
    {
        return await svc.DeleteAsync(id);
    }
}
