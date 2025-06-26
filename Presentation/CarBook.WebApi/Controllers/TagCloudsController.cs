using CarBook.Application.Common.Results.Concretes;
using CarBook.Application.Features.Commands.TagCloudCommands;
using CarBook.Application.Features.Queries.TagCloudQueries;
using CarBook.Application.Features.Results.SocialMediaResult;
using CarBook.Application.Features.Results.TagCloudResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagCloudsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TagCloudsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetTagClouds(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetTagCloudQuery(),cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<IEnumerable<GetTagCloudQueryResult>> errorDataResult)
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
        public async Task<IActionResult> GetTagCloudById(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetTagCloudByIdQuery(id), cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<GetTagCloudByIdQueryResult> errorDataResult)
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

        [HttpPost]
        public async Task<IActionResult> CreateTagCloud([FromBody] CreateTagCloudCommand command, CancellationToken cancellationToken)
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

        [HttpDelete]
        public async Task<IActionResult> RemoveTagCloud(RemoveTagCloudCommand command, CancellationToken cancellationToken)
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

        [HttpPut]
        public async Task<IActionResult> UpdateTagCloud([FromBody] UpdateTagCloudCommand command, CancellationToken cancellationToken)
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

        [HttpGet("GetTagCloudsByBlogId/{blogId}")]
        public async Task<IActionResult> GetTagCloudsByBlogId(int blogId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetTagCloudByBlogIdQuery(blogId),cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<IEnumerable<GetTagCloudByBlogIdQueryResult>> errorDataResult)
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
    }
}
