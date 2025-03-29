using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using LogisticsAid_API.Entities;
using LogisticsAid_API.DTOs;
using LogisticsAid_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsAid_API.Controllers;

[Authorize]
[Route("[controller]/[action]")]
[ApiController]
public class UserController : BaseController
{
    private readonly UserService _userService;
    private readonly AuthService _authService;

    public UserController(
        UserService userService,
        AuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    [HttpGet]
    public Task<ActionResult> GetUsers(CancellationToken ct) =>
        ExecuteSafely(async () =>
        {
            var users = await _userService.GetAllUsersAsync(ct);
            return Ok(users);
        });

    [HttpGet]
    public Task<ActionResult> GetUser(CancellationToken ct) =>
        ExecuteSafely(async () =>
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity == null)
                return Unauthorized("User identity is null");

            var idClaim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (idClaim == null)
                return Unauthorized("User ID claim is missing");

            if (!Guid.TryParse(idClaim.Value, out var userId))
                return Unauthorized("Invalid User ID format");

            var userDto = await _userService.GetUserByIdAsync(userId, ct);
            if (userDto == null)
                return NotFound("User not found");

            return Ok(userDto);
        });


    [HttpGet("{id}")]
    public Task<ActionResult> GetById(string id, CancellationToken ct) =>
        ExecuteSafely(async () =>
        {
            var userDto = await _userService.GetUserByIdAsync(Guid.Parse(id), ct);

            return Ok(userDto);
        });
    
    [AllowAnonymous]
    [HttpGet]
    public Task<ActionResult> IsAuthenticated()
    {
        if(User.Identity is { IsAuthenticated: true })
        {
            return Task.FromResult<ActionResult>(Ok(new { isAuthenticated = true }));
        }

        return Task.FromResult<ActionResult>(Unauthorized(new {isAuthenticated = false}));
    }

    [AllowAnonymous]
    [HttpPost]
    public Task<ActionResult> Register([FromBody] UserDTO user, CancellationToken ct) =>
        ExecuteSafely(async () =>
        {
            var createdUser = await _userService.CreateUserAsync(user, ct);
            
            // var accessToken = _authService.GenerateToken(createdUser);
            // var cookieOptions = _authService.GetCookieOptions();
            // HttpContext.Response.Cookies.Append("auth_token", accessToken, cookieOptions);

            return Ok(createdUser);
        });

    [AllowAnonymous]
    [HttpPost]
    public Task<ActionResult> Login(LoginDTO loginInfo, CancellationToken ct) =>
        ExecuteSafely(async () =>
        {
            var verifiedUser = await _userService.VerifyUserAsync(loginInfo, ct);

            var accessToken = _authService.GenerateToken(verifiedUser);
            var cookieOptions = _authService.GetCookieOptions();
            HttpContext.Response.Cookies.Append("auth_token", accessToken, cookieOptions);

            return Ok(verifiedUser);
        });

    [HttpPut]
    public Task<ActionResult> UpdateUser([FromBody] UserDTO user, CancellationToken ct) =>
        ExecuteSafely(async () =>
        {
            var updatedUser = await _userService.UpdateUserAsync(user, ct);

            return Ok(updatedUser);
        });

    [HttpDelete]
    public Task<ActionResult> Logout()
    {
        HttpContext.Response.Cookies.Delete("auth_token");
        
        return Task.FromResult<ActionResult>(Ok());
    }


    [HttpDelete("{id}")]
    public Task<ActionResult> Delete(string id, CancellationToken ct) =>
        ExecuteSafely(async () =>
        {
            await _userService.DeleteUserAsync(Guid.Parse(id), ct);
            return NoContent();
        });
}