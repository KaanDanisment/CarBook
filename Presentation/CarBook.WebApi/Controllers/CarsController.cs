using CarBook.Application.Features.Commands.CarCommands;
using CarBook.Application.Features.Queries.CarQueries;
using CarBook.Application.Features.Handlers.CarHandlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using CarBook.Application.Features.Results.CarResults;
using CarBook.Application.Common.Results.Concretes;
using Microsoft.AspNetCore.Authorization;

namespace CarBook.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCar(CreateCarCommand command, CancellationToken cancellationToken)
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

        [HttpGet]
        public async Task<IActionResult> GetCars(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCarsQuery(), cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<IEnumerable<GetCarsQueryResult>> errorDataResult)
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
        public async Task<IActionResult> GetCarById(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCarByIdQuery(id), cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<GetCarByIdQueryResult> errorDataResult)
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
        public async Task<IActionResult> UpdateCar(UpdateCarCommand command, CancellationToken cancellationToken)
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
        public async Task<IActionResult> RemoveCar(int id, CancellationToken cancellationToken)
        {
            RemoveCarCommand command = new RemoveCarCommand(id);
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


        [HttpGet("GetLatest5Cars")]
        public async Task<IActionResult> GetLatest5CarsWithBrand(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetLatest5CarsQuery(), cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<IEnumerable<GetLatest5CarsQueryResult>> errorDataResult)
                {
                    if (errorDataResult.ErrorType == "SystemError")
                    {
                        return StatusCode(500, errorDataResult.Message);
                    }
                }
            }
            return Ok(result.Data);
        }

        [HttpGet("GetAvailableCarsByLocation/{locationId}")]
        public async Task<IActionResult> GetAvailableCarsByLocation(CancellationToken cancellationToken, int locationId)
        {
            var result = await _mediator.Send(new GetAvailableCarsByLocationIdQuery(locationId), cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<IEnumerable<GetAvailableCarsByLocationIdQueryResult>> errorDataResult)
                {
                    if (errorDataResult.ErrorType == "SystemError")
                    {
                        return StatusCode(500, errorDataResult.Message);
                    }
                }
            }
            return Ok(result.Data);
        }

        [HttpGet("GetCarToRentByCarId/{carId}")]
        public async Task<IActionResult> GetCarToRentByCarId(CancellationToken cancellationToken, int carId)
        {
            var result = await _mediator.Send(new GetCarToRentByCarIdQuery(carId), cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<GetCarToRentByCarIdQueryResult> errorDataResult)
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
