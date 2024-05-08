using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using nettbutikk_api.Models.DTOs;
using nettbutikk_api.Models.Entities;
using nettbutikk_api.Services;
using nettbutikk_api.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace nettbutikk_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{

    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService=userService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDTO>> Register(UserRegDTO userRegDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _userService.RegisterAsync(userRegDTO);

        if (result != null)
        {
            return Ok(result);
        }
        return BadRequest("Failed to register user.");
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(UserLoginDTO userLoginDTO)
    {
        var token = await _userService.LoginAsync(userLoginDTO.Username, userLoginDTO.Password);

        if (token != null)
        {
            return Ok(token);
        }
        return Unauthorized("Feil brukernavn eller password");
    }
}
