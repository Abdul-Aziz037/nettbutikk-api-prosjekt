using System.Collections.Generic;
using System.Threading.Tasks;
using nettbutikk_api.Models.Entities;

namespace nettbutikk_api.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int userId);
        Task<User> AddUserAsync(User user);
        Task<User> UpdateUserAsync(int userId, User user);
        Task<User> DeleteUserByIdAsync(int userId);
    }
}
