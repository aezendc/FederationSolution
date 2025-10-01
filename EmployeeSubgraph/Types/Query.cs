using EmployeeSubgraph.Services;

namespace EmployeeSubgraph.Types;

public class Query
{
    [GraphQLName("getEmployees")]
    [GraphQLDescription("Get all employees.")]
    public async Task<List<Employee>> GetEmployees([Service] EmployeeService svc)
    {
        return await svc.GetAllAsync();
    }

    [GraphQLName("getEmployee")]
    [GraphQLDescription("Get a single employee by id.")]
    public async Task<Employee?> GetEmployee([ID] string id, [Service] EmployeeService svc)
    {
        return await svc.GetByIdAsync(id);
    }
}
