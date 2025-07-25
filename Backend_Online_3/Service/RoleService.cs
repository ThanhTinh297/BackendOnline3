using Backend_Online_3.Data;
using Backend_Online_3.Irepository;
using Backend_Online_3.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend_Online_3.Service
{
    public class RoleService : IRoleService
    {
        private readonly BackendOnline3DbContext _context;

        public RoleService(BackendOnline3DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetRoles()
        {
            return await _context.Role.ToListAsync();
        }

        public async Task<Role?> CreateRole(Role role)
        {
            _context.Role.Add(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<bool> UpdateRole(int id, Role role)
        {
            var existing = await _context.Role.FindAsync(id);
            if (existing == null) return false;

            _context.Entry(existing).CurrentValues.SetValues(role);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRole(int id)
        {
            var role = await _context.Role.FindAsync(id);
            if (role == null) return false;

            _context.Role.Remove(role);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
