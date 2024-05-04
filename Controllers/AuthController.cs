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
    //public static User user = new User();
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;

    public AuthController(IConfiguration configuration, IUserService userService)
    {
        _configuration=configuration;
        _userService=userService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDTO>> Register(UserRegDTO userRegDTO)
    {
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
        return Unauthorized("Invalid username or password.");
    }

    //[HttpPost("register")]
    //public ActionResult<User> Register(UserRegDTO request)
    //{
    //    string passwordHash
    //            = BCrypt.Net.BCrypt.HashPassword(request.Password);

    //    user.Username = request.Username;
    //    user.PasswordHash = passwordHash;

    //    return Ok(user);
    //}

    //[HttpPost("login")]
    //public ActionResult<User> Login(UserRegDTO request)
    //{
    //    if (user.Username != request.Username)
    //    {
    //        return BadRequest("User not found.");
    //    }

    //    if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
    //    {
    //        return BadRequest("Wrong password.");
    //    }
    //    string token = CreateToken(user);

    //    return Ok(token);
    //}
    //private string CreateToken(User user)
    //{
    //    List<Claim> claims = new List<Claim> {
    //            new Claim(ClaimTypes.Name, user.Username),
    //        };

    //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
    //        _configuration.GetSection("AppSettings:Token").Value!));

    //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

    //    var token = new JwtSecurityToken(
    //            claims: claims,
    //            expires: DateTime.Now.AddDays(1),
    //            signingCredentials: creds
    //        );

    //    var jwt = new JwtSecurityTokenHandler().WriteToken(token);

    //    return jwt;
    //}
}
