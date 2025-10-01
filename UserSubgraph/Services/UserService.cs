using Microsoft.EntityFrameworkCore;
using UserSubgraph.Data;
using UserSubgraph.Types;

namespace UserSubgraph.Services
{
    public class UserService
    {
        private readonly UserDbContext _db;

        public UserService(UserDbContext db)
        {
            _db = db;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var entities = await _db.Users.AsNoTracking().ToListAsync();
            return entities.Select(e => new User(e.UserId.ToString(), e.Username)).ToList();
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            if (!int.TryParse(id, out var intId)) return null;
            var entity = await _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == intId);
            if (entity is null) return null;
            return new User(entity.UserId.ToString(), entity.Username);
        }

        public async Task<User> AddUserAsync(string id, string username)
        {
            // Id will be ignored if identity; keep method signature for federation compatibility.
            var entity = new UserEntity { Username = username };
            _db.Users.Add(entity);
            await _db.SaveChangesAsync();
            return new User(entity.UserId.ToString(), entity.Username);
        }

        public async Task<User?> EditUserAsync(string id, string? username)
        {
            if (!int.TryParse(id, out var intId)) return null;
            var entity = await _db.Users.FirstOrDefaultAsync(u => u.UserId == intId);
            if (entity is null) return null;

            entity.Username = username ?? entity.Username;
            await _db.SaveChangesAsync();

            return new User(entity.UserId.ToString(), entity.Username);
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            if (!int.TryParse(id, out var intId)) return false;
            var entity = await _db.Users.FirstOrDefaultAsync(u => u.UserId == intId);
            if (entity is null) return false;

            _db.Users.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
