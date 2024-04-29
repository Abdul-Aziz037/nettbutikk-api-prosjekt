using AutoMapper;
using nettbutikk_api.Models.DTOs;
using nettbutikk_api.Models.Entities;
using nettbutikk_api.Repositories.Interfaces;
using nettbutikk_api.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nettbutikk_api.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<UserDTO>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetUsersAsync();
        var dto = users.Select(user => _mapper.Map<UserDTO>(user)).ToList();
        return dto;
    }

    public async Task<UserDTO?> GetUserByIdAsync(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        return user != null ? _mapper.Map<UserDTO>(user) : null;
    }

    public async Task<UserDTO?> AddUserAsync(UserDTO userDto)
    {
        var user = _mapper.Map<User>(userDto);

        var res = await _userRepository.AddUserAsync(user);
        return res != null ? _mapper.Map<UserDTO>(res) : null;
    }

    public async Task<UserDTO?> UpdateUserAsync(int userId, UserDTO userDto)
    {
        var user = _mapper.Map<User>(userDto);
        var updatedUser = await _userRepository.UpdateUserAsync(userId, user);

        if (updatedUser == null)
        {
            return null;
        }

        return _mapper.Map<UserDTO>(updatedUser);
    }

    public async Task<UserDTO?> DeleteUserAsync(int userId)
    {
        var deletedUser = await _userRepository.DeleteUserByIdAsync(userId);
        return deletedUser != null ? _mapper.Map<UserDTO>(deletedUser) : null;
    }
}

