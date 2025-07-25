using Backend_Online_3.Irepository;
using Backend_Online_3.Models;

namespace Backend_Online_3.Service
{
    public class UserService : IUserService
    {
        public Task<IEnumerable<User>> GetUsers()
        {
            throw new NotImplementedException();
        }
        public Task<User?> CreateUser(User user)
        {
            throw new NotImplementedException();
        }
        public Task<bool> UpdateUser(int id, User user)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteUser(int id)
        {
            throw new NotImplementedException();
        }
    }
    {
    }
}
