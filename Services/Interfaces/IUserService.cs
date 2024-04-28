using nettbutikk_api.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace nettbutikk_api.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int userId);
        Task<User> AddUserAsync(User user);
        Task<User> UpdateUserAsync(int userId, User user);
        Task<User> DeleteUserByIdAsync(int userId);
    }
}
