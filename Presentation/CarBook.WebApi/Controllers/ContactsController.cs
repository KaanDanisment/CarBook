using CarBook.Application.Features.Commands.ContactCommands;
using CarBook.Application.Features.Queries.ContactQueries;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using CarBook.Application.Features.Results.CategoryResults;
using CarBook.Application.Features.Results.ContactResults;
using CarBook.Application.Common.Results.Concretes;
using Microsoft.AspNetCore.Authorization;

namespace CarBook.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContactsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetContactQuery(), cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<IEnumerable<GetContactQueryResult>> errorDataResult)
                {
                    if (errorDataResult.ErrorType == "SystemError")
                    {
                        return StatusCode(500, errorDataResult.Message);
                    }
                }
            }
            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactById(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetContactByIdQuery(id), cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<GetContactByIdQueryResult> errorDataResult)
                {
                    if (errorDataResult.ErrorType == "SystemError")
                    {
                        return StatusCode(500, errorDataResult.Message);
                    }
                    else if (errorDataResult.ErrorType == "BadRequest")
                    {
                        return BadRequest(new { Message = errorDataResult.Message });
                    }
                }
            }
            return Ok(result.Data);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactCommand command, CancellationToken cancellationToken)
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

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateContact(UpdateContactCommand command, CancellationToken cancellationToken)
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
                    else if (errorResult.ErrorType == "BadRequest")
                    {
                        return BadRequest(new { Message = errorResult.Message });
                    }
                }
            }
            return Ok(new { Message = result.Message });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> RemoveContact(RemoveContactCommand command, CancellationToken cancellationToken)
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
                    else if (errorResult.ErrorType == "BadRequest")
                    {
                        return BadRequest(new { Message = errorResult.Message });
                    }
                }
            }
            return Ok(new { Message = result.Message });
        }
    }
}
