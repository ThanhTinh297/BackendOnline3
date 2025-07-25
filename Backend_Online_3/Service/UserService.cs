using Backend_Online_3.Data;
using Backend_Online_3.Irepository;
using Backend_Online_3.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend_Online_3.Service
{
    public class UserService : IUserService
    {
        private readonly BackendOnline3DbContext _context;

        public UserService(BackendOnline3DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<User?> CreateUser(User user)
        {
            if (user.DateOfBirth.HasValue)
            {
                user.DateOfBirth = DateTime.SpecifyKind(user.DateOfBirth.Value, DateTimeKind.Utc);
            }

            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }


        public async Task<bool> UpdateUser(int id, User user)
        {
            var existing = await _context.User.FindAsync(id);
            if (existing == null) return false;

            if (user.DateOfBirth.HasValue)
            {
                user.DateOfBirth = DateTime.SpecifyKind(user.DateOfBirth.Value, DateTimeKind.Utc);
            }

            _context.Entry(existing).CurrentValues.SetValues(user);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null) return false;

            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
