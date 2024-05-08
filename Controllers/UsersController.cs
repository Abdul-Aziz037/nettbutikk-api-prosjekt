using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return users != null && users.Count > 0 ? Ok(users) : NotFound("Brukeren ble ikke funnet");
    }

    [HttpGet("{userId}", Name = "GetUserById")]
    [Authorize]
    public async Task<ActionResult<UserDTO>> GetUserById(int userId)
    {
        var user = await _userService.GetUserByIdAsync(userId);
        return user != null ? Ok(user) : NotFound("Brukeren ble ikke funnet");
    }

    [HttpPut("{userId}", Name = "UpdateUser")]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<UserDTO>> UpdateUser(int userId, UserDTO userDto)
    {
        var loggedInUserName = HttpContext.User.Identity?.Name!;
        var loggedInUser = await _userService.GetUserByNameAsync(loggedInUserName);

        var userToUpdate = await _userService.GetUserByIdAsync(userId);

        if (userToUpdate == null || (userToUpdate.UserId != loggedInUser?.UserId && !User.IsInRole("Admin")))
        {
            return Forbid();
        }

        var updatedUser = await _userService.UpdateUserAsync(userId, userDto);
        return updatedUser != null ? Ok(updatedUser) : NotFound("Brukeren ble ikke funnet");
    }

    [HttpDelete("{userId}", Name = "DeleteUser")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> DeleteUser(int userId)
    {
        var loggedInUserName = HttpContext.User.Identity?.Name!;
        var loggedInUser = await _userService.GetUserByNameAsync(loggedInUserName);


        var userToDelete = await _userService.GetUserByIdAsync(userId);


        if (userToDelete == null || (userToDelete.UserId != loggedInUser?.UserId && !User.IsInRole("Admin")))
        {
            return Forbid();
        }
        var deletedUser = await _userService.DeleteUserAsync(userId);
        return deletedUser != null ? Ok(userToDelete) : NotFound("Brukeren ble ikke funnet");
    }
}
