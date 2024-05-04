using nettbutikk_api.Models.DTOs;
using nettbutikk_api.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace nettbutikk_api.Services.Interfaces
{
    public interface IUserService
    {
        Task<ICollection<UserDTO>> GetAllUsersAsync();
        Task<UserDTO?> GetUserByIdAsync(int userId);
        Task<UserDTO?> GetUserByNameAsync(string userName);
        //Task<UserDTO?> AddUserAsync(UserDTO user);
        Task<UserDTO?> RegisterAsync(UserRegDTO userRegDTO);
        Task<UserDTO?> UpdateUserAsync(int userId, UserDTO user);
        Task<UserDTO?> DeleteUserAsync(int userId);

        Task<string?> LoginAsync(string userName, string password);
    }
}
