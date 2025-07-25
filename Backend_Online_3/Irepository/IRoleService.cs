using Backend_Online_3.Models;

namespace Backend_Online_3.Irepository
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetRoles();
        Task<Role?> CreateRole(Role role);
        Task<bool> UpdateRole(int id, Role role);
        Task<bool> DeleteRole(int id);
    }
}
