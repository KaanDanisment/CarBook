using CarBook.Application.Common.Results.Concretes;
using CarBook.Application.Features.Commands.CommentCommands;
using CarBook.Application.Features.Queries.CarQueries;
using CarBook.Application.Features.Queries.CommentQueries;
using CarBook.Application.Features.Results.CommentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(CreateCommentCommand command, CancellationToken cancellationToken)
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCommentByIdQuery(id), cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<GetCommentByIdQueryResult> errorDataResult)
                {
                    if (errorDataResult.ErrorType == "BadRequest")
                    {
                        return BadRequest(new { Message = errorDataResult.Message });
                    }
                    else if (errorDataResult.ErrorType == "SystemErro")
                    {
                        return StatusCode(500, errorDataResult.Message);
                    }
                }
            }
            return Ok(result.Data);
        }


        [HttpGet]
        public async Task<IActionResult> GetComments(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCommentsQuery(), cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<IEnumerable<GetCommentQueryResult>> errorDataResult)
                {
                    if (errorDataResult.ErrorType == "SystemError")
                    {
                        return StatusCode(500, errorDataResult.Message);
                    }
                }
            }
            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveComment(int id, CancellationToken cancellationToken)
        {
            RemoveCommentCommand command = new RemoveCommentCommand(id);
            var result = await _mediator.Send(command, cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorResult errorResult)
                {
                    if (errorResult.ErrorType == "BadRequest")
                    {
                        return BadRequest(new { Message = errorResult.Message });
                    }
                    else if (errorResult.ErrorType == "SystemError")
                    {
                        return StatusCode(500, errorResult.Message);
                    }
                }
            }
            return Ok(new { Message = result.Message });
        }


        [HttpPut]
        public async Task<IActionResult> UpdateComment(UpdateCommentCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorResult errorResult)
                {
                    if (errorResult.ErrorType == "BadRequest")
                    {
                        return BadRequest(new { Message = errorResult.Message });
                    }
                    else if (errorResult.ErrorType == "SystemError")
                    {
                        return StatusCode(500, errorResult.Message);
                    }
                }
            }
            return Ok(new { Message = result.Message });
        }

        [HttpGet("GetCommentsByBlogId/{id}")]
        public async Task<IActionResult> GetCommentsByBlogId(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCommentsByBlogIdQuery(id), cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<IEnumerable<GetCommentsByBlogIdQueryResult>> errorDataResult)
                {
                    if (errorDataResult.ErrorType == "BadRequest")
                    {
                        return BadRequest(new { Message = errorDataResult.Message });
                    }
                    else if (errorDataResult.ErrorType == "SystemError")
                    {
                        return StatusCode(500, errorDataResult.Message);
                    }
                }
            }
            return Ok(result.Data);
        }

        [HttpGet("GetCommentsCountByBlogId/{blogId}")]
        public async Task<IActionResult> GetCommentsCountByBlogId(int blogId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCommentsCountByBlogIdQuery(blogId), cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<GetCommentsCountByBlogIdQueryResult> errorDataResult)
                {
                    if (errorDataResult.ErrorType == "BadRequest")
                    {
                        return BadRequest(new { Message = errorDataResult.Message });
                    }
                    else if (errorDataResult.ErrorType == "SystemError")
                    {
                        return StatusCode(500, errorDataResult.Message);
                    }
                }
            }
            return Ok(result.Data);
        }
    }
}
