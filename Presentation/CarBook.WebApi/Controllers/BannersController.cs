using CarBook.Application.Features.Commands.BannerCommands;
using CarBook.Application.Features.Queries.BannerQueries;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using CarBook.Application.Features.Results.BannerResults;
using CarBook.Application.Common.Results.Concretes;
using Microsoft.AspNetCore.Authorization;

namespace CarBook.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BannersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateBanner(CreateBannerCommand createBannerCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(createBannerCommand, cancellationToken);
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
        public async Task<IActionResult> GetAllBanners(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetBannerQuery(), cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<IEnumerable<GetBannerQueryResult>> errorResult)
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
        public async Task<IActionResult> GetBannerById(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetBannerByIdQuery(id), cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<GetBannerByIdQueryResult> errorDataResult)
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveBanner(int id, CancellationToken cancellationToken)
        {
            RemoveBannerCommand command = new RemoveBannerCommand(id);
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
        public async Task<IActionResult> UpdateBanner(UpdateBannerCommand updateBannerCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(updateBannerCommand, cancellationToken);
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
            return Ok(new {Message = result.Message});
        }
    }
}
