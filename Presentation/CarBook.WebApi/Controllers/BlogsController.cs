using CarBook.Application.Common.Results.Concretes;
using CarBook.Application.Features.Commands.BlogCommands;
using CarBook.Application.Features.Queries.BlogQueries;
using CarBook.Application.Features.Results.BlogResult;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BlogsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetBlog(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetBlogsQuery(), cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<IEnumerable<GetBlogsQueryResult>> errorDataResult)
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
        public async Task<IActionResult> GetBlogById(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetBlogByIdQuery(id), cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<GetBlogByIdQueryResult> errorDataResult)
                {
                    if (errorDataResult.ErrorType == "SystemError")
                    {
                        return StatusCode(500, errorDataResult.Message);
                    }
                    else if (errorDataResult.ErrorType == "BadRequest")
                    {
                        return BadRequest(errorDataResult.Message);
                    }
                }
            }
            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog(CreateBlogCommand command, CancellationToken cancellationToken)
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
        public async Task<IActionResult> UpdateBlog(UpdateBlogCommand command, CancellationToken cancellationToken)
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
                        return BadRequest(new { Message = result.Message });
                    }
                }
            }
            return Ok(new { Message = result.Message });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveBlog(int id, CancellationToken cancellationToken)
        {
            RemoveBlogCommand command = new RemoveBlogCommand(id);
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
                        return BadRequest(new { Message = result.Message });
                    }
                }
            }
            return Ok(new { Message = result.Message });
        }

        [HttpGet("GetBlogsWithAuthor")]
        public async Task<IActionResult> GetBlogsWithAuthor(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetBlogsWithAuthorQuery(), cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<IEnumerable<GetBlogsWithAuthorQueryResult>> errorDataResult)
                {
                    if (errorDataResult.ErrorType == "SystemError")
                    {
                        return StatusCode(500, errorDataResult.Message);
                    }
                }
            }
            return Ok(result.Data);
        }

        [HttpGet("GetLatest3BlogsWithAuthor")]
        public async Task<IActionResult> GetLatest3BlogsWithAuthor(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetLatest3BlogsWithAuthorQuery(), cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<IEnumerable<GetLatest3BlogsWithAuthorQueryResult>> errorDataResult)
                {
                    if (errorDataResult.ErrorType == "SystemError")
                    {
                        return StatusCode(500, errorDataResult.Message);
                    }
                }
            }
            return Ok(result.Data);
        }
    }
}
