﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using HealthQ_API.DTOs;
using HealthQ_API.Entities;
using HealthQ_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthQ_API.Controllers;

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
    public Task<ActionResult> Get(CancellationToken ct) =>
        ExecuteSafely(async () =>
        {
            var users = await _userService.GetAllUsersAsync(ct);
            return Ok(users);
        });

    [HttpGet]
    public Task<ActionResult> GetUser(CancellationToken ct) =>
        ExecuteSafely(async () =>
        {
            var email = (User.Identity as ClaimsIdentity)!.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
                return Unauthorized();

            var userDto = await _userService.GetUserByEmailAsync(email, ct);

            return Ok(userDto);
        });

    [HttpGet("{email}")]
    public Task<ActionResult> GetById(string email, CancellationToken ct) =>
        ExecuteSafely(async () =>
        {
            var userDto = await _userService.GetUserByEmailAsync(email, ct);

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

            var accessToken = _authService.GenerateToken(createdUser.Email);
            var cookieOptions = _authService.GetCookieOptions(createdUser.Email);
            HttpContext.Response.Cookies.Append("auth_token", accessToken, cookieOptions);

            return Ok(createdUser);
        });

    [AllowAnonymous]
    [HttpPost]
    public Task<ActionResult> Login(UserDTO user, CancellationToken ct) =>
        ExecuteSafely(async () =>
        {
            var verifiedUser = await _userService.VerifyUserAsync(user, ct);

            var accessToken = _authService.GenerateToken(verifiedUser.Email);
            var cookieOptions = _authService.GetCookieOptions(verifiedUser.Email);
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


    [HttpDelete("{email}")]
    public Task<ActionResult> Delete(string email, CancellationToken ct) =>
        ExecuteSafely(async () =>
        {
            await _userService.DeleteUserAsync(email, ct);
            return NoContent();
        });
}