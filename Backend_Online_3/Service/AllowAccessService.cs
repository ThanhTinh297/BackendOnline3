using Backend_Online_3.Data;
using Backend_Online_3.Irepository;
using Backend_Online_3.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend_Online_3.Service
{
    public class AllowAccessService : IAllowAccessService
    {
        private readonly BackendOnline3DbContext _context;

        public AllowAccessService(BackendOnline3DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AllowAccess>> GetAllowAccesses()
        {
            return await _context.AllowAccess.ToListAsync();
        }

        public async Task<AllowAccess?> CreateAllowAccess(AllowAccess allowAccess)
        {
            _context.AllowAccess.Add(allowAccess);
            await _context.SaveChangesAsync();
            return allowAccess;
        }

        public async Task<bool> UpdateAllowAccess(int id, AllowAccess allowAccess)
        {
            var existing = await _context.AllowAccess.FindAsync(id);
            if (existing == null) return false;

            _context.Entry(existing).CurrentValues.SetValues(allowAccess);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAllowAccess(int id)
        {
            var access = await _context.AllowAccess.FindAsync(id);
            if (access == null) return false;

            _context.AllowAccess.Remove(access);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
