using CarBook.Application.Features.Commands.AboutCommands;
using CarBook.Application.Features.Queries.AboutQueries;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Threading;
using CarBook.Application.Features.Results.AboutResults;
using CarBook.Application.Common.Results.Concretes;
using Microsoft.AspNetCore.Authorization;

namespace CarBook.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AboutsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorResult errorResult)
                {
                    if (errorResult.ErrorType == "SystemError")
                    {
                        return StatusCode(500, errorResult.Message);
                    }
                }
            }
            return Ok(new { Message = result.Message });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAbouts(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAboutQuery(), cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<IEnumerable<GetAboutQueryResult>> errorResult)
                {
                    if (errorResult.ErrorType == "SystemError")
                    {
                        return StatusCode(500, errorResult.Message);
                    }
                }
            }
            
            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAboutById(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAboutByIdQuery(id), cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<GetAboutByIdQueryResult> errorResult)
                {
                    if (errorResult.ErrorType == "SystemError")
                    {
                        return StatusCode(500, errorResult.Message);
                    }
                    else if (errorResult.ErrorType == "BadRequest")
                    {
                        return BadRequest(new { Message = errorResult.Message });
                    }
                }
            }
            return Ok(result.Data);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id, CancellationToken cancellationToken)
        {
            RemoveAboutCommand command = new RemoveAboutCommand(id);
            var result = await _mediator.Send(command, cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorResult errorResult)
                {
                    if (errorResult.ErrorType == "SystemError")
                    {
                        return StatusCode(500, errorResult.Message);
                    }
                    else if (errorResult.ErrorType == "BadRequest")
                    {
                        return BadRequest(new { Message = errorResult.Message });
                    }
                }
            }
            return Ok(new { Message = result.Message });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateAboutCommand updateAboutCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(updateAboutCommand, cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorResult errorResult)
                {
                    if (errorResult.ErrorType == "SystemError")
                    {
                        return StatusCode(500, errorResult.Message);
                    }
                    if (errorResult.ErrorType == "BadRequest")
                    {
                        return BadRequest(new { Message = errorResult.Message });
                    }
                }
            }
            return Ok(new { Message = result.Message });
        }
    }
}
