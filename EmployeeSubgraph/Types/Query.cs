using EmployeeSubgraph.Services;

namespace EmployeeSubgraph.Types;

public class Query
{
    [GraphQLName("getEmployees")]
    [GraphQLDescription("Get all employees.")]
    public Task<List<Employee>> GetEmployees([Service] EmployeeService svc) => svc.GetAllAsync();

    [GraphQLName("getEmployee")]
    [GraphQLDescription("Get a single employee by id.")]
    public Task<Employee?> GetEmployee([ID] string id, [Service] EmployeeService svc) => svc.GetByIdAsync(id);
}
