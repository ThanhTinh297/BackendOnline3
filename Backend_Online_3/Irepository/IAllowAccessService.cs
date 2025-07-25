using Backend_Online_3.Models;

namespace Backend_Online_3.Irepository
{
    public interface IAllowAccessService
    {
        Task<IEnumerable<AllowAccess>> GetAllowAccesses();
        Task<AllowAccess?> CreateAllowAccess(AllowAccess allowAccess);
        Task<bool> UpdateAllowAccess(int id, AllowAccess allowAccess);
        Task<bool> DeleteAllowAccess(int id);
    }
}
