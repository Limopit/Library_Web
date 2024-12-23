﻿using Library.Application.Users.Commands.AssignRole;
using Library.Application.Users.Commands.LoginUser;
using Library.Application.Users.Commands.RefreshToken;
using Library.Application.Users.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebAPI.Controllers;

[Route("api/[controller]")]
public class AuthController: BaseController
{
    public AuthController(IMediator mediator) : base(mediator) {}
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        var userId = await _mediator.Send(command);
        return Ok(new { UserId = userId });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var (jwt, refresh) = await _mediator.Send(command);
        return Ok(new { jwt, refresh });
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost("assign-role")]
    public async Task<IActionResult> AssignRole([FromBody] AssignRoleCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [Authorize]
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
    {
        var jwt = await _mediator.Send(command); 
        return Ok(jwt);
    }
}