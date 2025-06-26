using CarBook.Application.Common.Results.Concretes;
using CarBook.Application.Features.Commands.AppUserCommands;
using CarBook.Application.Features.Queries.AppUserQueries;
using CarBook.Application.Features.Results.AppUserResult;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] GetAppUserQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<GetAppUserQueryResult> errorResult)
                {
                    if (errorResult.ErrorType == "Unauthorize")
                    {
                        return Unauthorized(errorResult.Message);
                    }
                    else if (errorResult.ErrorType == "SystemError")
                    {
                        return StatusCode(500, errorResult.Message);
                    }
                }
                return BadRequest(result.Message);
            }
            return Ok(result.Data.AccessToken);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterAppUserCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorResult errorResult)
                {
                    if (errorResult.ErrorType == "BadRequest")
                    {
                        return BadRequest(errorResult.Message);
                    }
                    else if (errorResult.ErrorType == "SystemError")
                    {
                        return StatusCode(500, errorResult.Message);
                    }
                }
                return BadRequest(result.Message);
            }
            return Ok(result.Message);
        }
    }
}
