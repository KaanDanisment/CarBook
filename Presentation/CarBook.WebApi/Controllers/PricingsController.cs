using CarBook.Application.Common.Results.Concretes;
using CarBook.Application.Features.Commands.PricingCommands;
using CarBook.Application.Features.Queries.PricingQueries;
using CarBook.Application.Features.Results.FooterAddressResult;
using CarBook.Application.Features.Results.PricingResult;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PricingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPricing(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetPricingQuery(), cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<IEnumerable<GetPricingQueryResult>> errorDataResult)
                {
                    if (errorDataResult.ErrorType == "SystemError")
                    {
                        return StatusCode(500, errorDataResult.Message);
                    }
                }
            }
            return Ok(result.Data);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreatePricing(CreatePricingCommand command, CancellationToken cancellationToken)
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
        public async Task<IActionResult> GetPricingById(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetPricingByIdQuery(id), cancellationToken);
                if (!result.Success)
                {
                    if (result is ErrorDataResult<GetPricingByIdQueryResult> errorDataResult)
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
        [HttpPut]
        public async Task<IActionResult> UpdatePricing(UpdatePricingCommand command, CancellationToken cancellationToken)
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePricing(int id, CancellationToken cancellationToken)
        {
            RemovePricingCommand command = new RemovePricingCommand(id);
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
