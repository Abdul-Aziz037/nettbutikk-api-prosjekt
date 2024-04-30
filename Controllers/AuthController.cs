using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using nettbutikk_api.Models.DTOs;
using nettbutikk_api.Models.Entities;
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
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(UserRegDTO request)
    {
        var result = await _userService.RegisterAsync(request);
        if (result != null)
        {
            return Ok(result);
        }
        return BadRequest("Failed to register user.");
    }

    //[HttpPost("login")]
    //public ActionResult<User> Login(UserRegDTO request)
    //{
    //    if(user.Username != request.Username)
    //    {
    //        return BadRequest("User not found");
    //    }

    //    if(!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
    //    {
    //        return BadRequest("Wrong password.");
    //    }

    //    string token = CreateToken(user);

    //    return Ok(token);   
    //}

    //private string CreateToken(User user)
    //{
    //    List<Claim> claims = new List<Claim>
    //    {
    //        new Claim(ClaimTypes.Name, user.Username)
    //    };

    //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
    //        _config.GetSection("AppSettings:Token").Value!));

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
