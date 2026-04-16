namespace InnoclinicAutho.WebApi.Controllers;

using InnoclinicAutho.Application.UseCases.Users.Commands.LoginUser;
using InnoclinicAutho.Application.UseCases.Users.Commands.LogoutUser;
using InnoclinicAutho.Application.UseCases.Users.Commands.RegisterUser;
using InnoclinicAutho.Application.UseCases.Users.Commands.RegisterUser.Admin;
using InnoclinicAutho.Application.UseCases.Users.Commands.RegisterUser.CreateUserByAdmin;
using InnoclinicAutho.Application.UseCases.Users.Queries;
using InnoclinicAutho.Application.UseCases.Users.Queries.GetUser;
using InnoclinicAutho.Application.UseCases.Users.Queries.GetUserList;
using InnoclinicAutho.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("sign-up")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SignUp([FromBody] RegisterUserCommand command)
    {
        if (command is null) return BadRequest();

        var response = await _mediator.Send(command);

        if (response.Succcess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpPost("create-user-by-admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateUserByAdmin([FromBody] CreateUserByAdminCommand command)
    {
        if (command is null) return BadRequest();

        var response = await _mediator.Send(command);

        if (response.Succcess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpPost("sign-up/admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SignUpAdmin([FromBody] RegisterAdminCommand command)
    {
        if (command is null) return BadRequest();

        var response = await _mediator.Send(command);

        if (response.Succcess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpPost("sign-in")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> SignIn([FromBody] LoginUserCommand command)
	{
		if (command is null) return BadRequest();

        var response = await _mediator.Send(command);

        if (response.Succcess)
        {
            return Ok(response);
        }

        return BadRequest(response);
	}

    [HttpPost("sign-out")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SignOut([FromBody] LogoutUserCommand command)
    {
        if (command is null) return BadRequest();

        var response = await _mediator.Send(command);
		
        if (response.Succcess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [Authorize]
    [HttpGet("Demo")]
    public IActionResult Demo()
    {
        return Ok("User Authenticated Successfully! Jwt Token is valid.");
    }

    [HttpGet("user-list")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUserList([FromQuery] GetUserListQuery query)
    {
        //if (query is null) return BadRequest();

        var response = await _mediator.Send(query);

        if (response.Succcess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpGet("user-profile")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUserProfile([FromQuery] GetUserQuery query)
    {
        if (query is null) return BadRequest();

        var response = await _mediator.Send(query);

        if (response.Succcess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}
