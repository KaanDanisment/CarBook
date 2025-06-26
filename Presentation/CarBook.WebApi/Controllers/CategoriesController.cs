using CarBook.Application.Features.Commands.CategoryCommands;
using CarBook.Application.Features.Queries.CategoryQueries;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using CarBook.Application.Features.Results.CategoryResults;
using CarBook.Application.Common.Results.Concretes;

namespace CarBook.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryCommand command, CancellationToken cancellationToken)
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
        public async Task<IActionResult> GetCategory(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCategoryQuery(), cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<IEnumerable<GetCategoryQueryResult>> errorDataResult)
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
        public async Task<IActionResult> GetCategoryById(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCategoryByIdQuery(id),cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<GetCategoryByIdQueryResult> errorDataResult)
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
        public async Task<IActionResult> UpdateCategory(UpdateCategoryCommand command, CancellationToken cancellationToken)
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
        public async Task<IActionResult> RemoveCategory(int id, CancellationToken cancellationToken)
        {
            RemoveCategoryCommand command = new RemoveCategoryCommand(id);
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
