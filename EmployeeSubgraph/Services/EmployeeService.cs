using Microsoft.EntityFrameworkCore;
using EmployeeSubgraph.Data;
using EmployeeSubgraph.Types;

namespace EmployeeSubgraph.Services;

public class EmployeeService
{
    private readonly EmployeeDbContext _db;
    public EmployeeService(EmployeeDbContext db) => _db = db;

    public async Task<List<Employee>> GetAllAsync()
    {
        var entities = await _db.Employees.AsNoTracking().ToListAsync();
        return entities.Select(e => new Employee(e.EmployeeId.ToString(), e.UserId.ToString(), e.FirstName, e.LastName)).ToList();
    }

    public async Task<Employee?> GetByIdAsync(string id)
    {
        if (!int.TryParse(id, out var intId)) return null;
        var entity = await _db.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.EmployeeId == intId);
        if (entity is null) return null;
        return new Employee(entity.EmployeeId.ToString(), entity.UserId.ToString(), entity.FirstName, entity.LastName);
    }

    public async Task<Employee> AddAsync(int userId, string? firstName, string? lastName)
    {
        var entity = new EmployeeEntity { UserId = userId, FirstName = firstName, LastName = lastName };
        _db.Employees.Add(entity);
        await _db.SaveChangesAsync();
        return new Employee(entity.EmployeeId.ToString(), entity.UserId.ToString(), entity.FirstName, entity.LastName);
    }

    public async Task<Employee?> EditAsync(string id, string? firstName, string? lastName)
    {
        if (!int.TryParse(id, out var intId)) return null;
        var entity = await _db.Employees.FirstOrDefaultAsync(e => e.EmployeeId == intId);
        if (entity is null) return null;

        entity.FirstName = firstName ?? entity.FirstName;
        entity.LastName = lastName ?? entity.LastName;

        await _db.SaveChangesAsync();
        return new Employee(entity.EmployeeId.ToString(), entity.UserId.ToString(), entity.FirstName, entity.LastName);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        if (!int.TryParse(id, out var intId)) return false;
        var entity = await _db.Employees.FirstOrDefaultAsync(e => e.EmployeeId == intId);
        if (entity is null) return false;

        _db.Employees.Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }
}
