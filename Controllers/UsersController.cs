using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nettbutikk_api.Models.DTOs;
using nettbutikk_api.Services.Interfaces;

namespace nettbutikk_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet(Name = "GetUsers")]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return users != null && users.Count > 0 ? Ok(users) : NotFound("No users found");
    }

    [HttpGet("{userId}", Name = "GetUserById")]
    public async Task<ActionResult<UserDTO>> GetUserById(int userId)
    {
        var user = await _userService.GetUserByIdAsync(userId);
        return user != null ? Ok(user) : NotFound("User not found");
    }

    [HttpPut("{userId}", Name = "UpdateUser")]
    public async Task<ActionResult<UserDTO>> UpdateUser(int userId, UserDTO userDto)
    {
        var updatedUser = await _userService.UpdateUserAsync(userId, userDto);
        return updatedUser != null ? Ok(updatedUser) : NotFound("User not found");
    }

    [HttpDelete("{userId}", Name = "DeleteUser")]
    public async Task<IActionResult> DeleteUser(int userId)
    {
        var deletedUser = await _userService.DeleteUserAsync(userId);
        return deletedUser != null ? NoContent() : NotFound("User not found");
    }
}
