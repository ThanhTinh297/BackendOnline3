using Backend_Online_3.Models;

namespace Backend_Online_3.Irepository
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User?> CreateUser(User user);
        Task<bool> UpdateUser(int id, User user);
        Task<bool> DeleteUser(int id);
    }
}
