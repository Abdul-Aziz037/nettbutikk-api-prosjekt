using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using nettbutikk_api.Models.DTOs;
using nettbutikk_api.Models.Entities;
using nettbutikk_api.Repositories.Interfaces;
using nettbutikk_api.Services.Interfaces;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace nettbutikk_api.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _userMapper;
    private readonly IMapper _userRegMapper;
    private readonly IConfiguration _config;
    public UserService(IUserRepository userRepository, IMapper mapper, IConfiguration config)
    {
        _userRepository = userRepository;
        _userMapper = mapper;
        _userRegMapper = mapper;
        _config=config;
    }

    public async Task<ICollection<UserDTO>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetUsersAsync();
        var dto = users.Select(user => _userMapper.Map<UserDTO>(user)).ToList();
        return dto;
    }

    public async Task<UserDTO?> GetUserByIdAsync(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        return user != null ? _userMapper.Map<UserDTO>(user) : null;
    }

    //public async Task<UserDTO?> AddUserAsync(UserDTO userDto)
    //{
    //    var user = _mapper.Map<User>(userDto);

    //    var res = await _userRepository.AddUserAsync(user);
    //    return res != null ? _mapper.Map<UserDTO>(res) : null;
    //}


    public async Task<UserDTO?> UpdateUserAsync(int userId, UserDTO userDto)
    {
        var user = _userMapper.Map<User>(userDto);
        var updatedUser = await _userRepository.UpdateUserAsync(userId, user);

        if (updatedUser == null)
        {
            return null;
        }

        return _userMapper.Map<UserDTO>(updatedUser);
    }

    public async Task<UserDTO?> DeleteUserAsync(int userId)
    {
        var deletedUser = await _userRepository.DeleteUserByIdAsync(userId);
        return deletedUser != null ? _userMapper.Map<UserDTO>(deletedUser) : null;
    }

    public async Task<UserDTO?> RegisterAsync(UserRegDTO userRegDTO)
    {
        var user = _userRegMapper.Map<User>(userRegDTO);
        // string passwordHash = BCrypt.Net.BCrypt.HashPassword(userRegDto.Password);
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userRegDTO.Password);

        var res = await _userRepository.AddUserAsync(user);

        return _userMapper.Map<UserDTO?>(res!);
    }

    public async Task<string?> LoginAsync(string userName, string password)
    {
        var user = await _userRepository.GetUserByNameAsync(userName);

        if (user == null)
        {
            //bruker ble ikke funnet
            return null;
        }

        if (BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            var token = GenerateJwtToken(user);
            return token;
        }
        //feil password
        return null;
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
        new Claim(ClaimTypes.Name, user.Username)
        // Legg til andre relevante krav etter behov
    };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(2), // Token utløper om 2 timer
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}

