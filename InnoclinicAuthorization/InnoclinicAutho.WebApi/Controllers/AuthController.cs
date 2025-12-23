namespace InnoclinicAutho.WebApi.Controllers;

using InnoclinicAutho.Application.UseCases.Users.Commands;
using MediatR;
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

}
