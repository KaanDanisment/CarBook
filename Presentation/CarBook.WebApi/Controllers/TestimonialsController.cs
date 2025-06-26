using CarBook.Application.Common.Results.Concretes;
using CarBook.Application.Features.Commands.TestimonialCommands;
using CarBook.Application.Features.Queries.TestimonialQueries;
using CarBook.Application.Features.Results.TagCloudResults;
using CarBook.Application.Features.Results.TestimonialResult;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TestimonialsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTestimonial(CreateTestimonialCommand command, CancellationToken cancellationToken)
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
        public async Task<IActionResult> GetTestimonials(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetTestimonialQuery(), cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<IEnumerable<GetTestimonialQueryResult>> errorDataResult)
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
        public async Task<IActionResult> GetTestimonialById(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetTestimonialByIdQuery(id), cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<GetTestimonialByIdQueryResult> errorDataResult)
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

        [HttpPut]
        public async Task<IActionResult> UpdateTestimonial(UpdateTestimonialCommand command, CancellationToken cancellationToken)
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveTestimonial(int id, CancellationToken cancellationToken)
        {
            RemoveTestimonialCommand command = new RemoveTestimonialCommand(id);
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
