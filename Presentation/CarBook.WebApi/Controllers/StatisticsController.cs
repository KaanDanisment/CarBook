using CarBook.Application.Common.Results.Concretes;
using CarBook.Application.Features.Queries.StatisticsQueries;
using CarBook.Application.Features.Results.StatisticsResult;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StatisticsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetCarsCount")]
        public async Task<IActionResult> GetCarsCount(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCarsCountQuery(), cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<GetCarsCountQueryResult> errorDataResult)
                {
                    if (errorDataResult.ErrorType == "SystemError")
                    {
                        return StatusCode(500, errorDataResult.Message);
                    }
                }
            }
            return Ok(result.Data);
        }

        [HttpGet("GetLocationsCount")]
        public async Task<IActionResult> GetLocationsCount(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetLocationsCountQuery(), cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<GetLocationsCountQueryResult> errorDataResult)
                {
                    if (errorDataResult.ErrorType == "SystemError")
                    {
                        return StatusCode(500, errorDataResult.Message);
                    }
                }
            }
            return Ok(result.Data);
        }

        [HttpGet("GetAuthorsCount")]
        public async Task<IActionResult> GetAuthorsCount(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAuthorsCountQuery(), cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<GetAuthorsCountQueryResult> errorDataResult)
                {
                    if (errorDataResult.ErrorType == "SystemError")
                    {
                        return StatusCode(500, errorDataResult.Message);
                    }
                }
            }
            return Ok(result.Data);
        }

        [HttpGet("GetBlogsCount")]
        public async Task<IActionResult> GetBlogsCount(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetBlogsCountQuery(), cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<GetBlogsCountQueryResult> errorDataResult)
                {
                    if (errorDataResult.ErrorType == "SystemError")
                    {
                        return StatusCode(500, errorDataResult.Message);
                    }
                }
            }
            return Ok(result.Data);
        }

        [HttpGet("GetBrandsCount")]
        public async Task<IActionResult> GetBrandsCount(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetBrandsCountQuery(), cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<GetBrandsCountQueryResult> errorDataResult)
                {
                    if (errorDataResult.ErrorType == "SystemError")
                    {
                        return StatusCode(500, errorDataResult.Message);
                    }
                }
            }
            return Ok(result.Data);
        }

        [HttpGet("GetAvgDailyRentalPrice")]
        public async Task<IActionResult> GetAvgDailyRentalPrice(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAvgDailyRentalPriceQuery(), cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<GetAvgDailyRentalPriceQueryResult> errorDataResult)
                {
                    if (errorDataResult.ErrorType == "SystemError")
                    {
                        return StatusCode(500, errorDataResult.Message);
                    }
                }
            }
            return Ok(result.Data);
        }

        [HttpGet("GetAvgWeeklyRentalPrice")]
        public async Task<IActionResult> GetAvgWeeklyRentalPrice(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAvgWeeklyRentalPriceQuery(), cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<GetAvgWeeklyRentalPriceQueryResult> errorDataResult)
                {
                    if (errorDataResult.ErrorType == "SystemError")
                    {
                        return StatusCode(500, errorDataResult.Message);
                    }
                }
            }
            return Ok(result.Data);
        }

        [HttpGet("GetAvgMonthlyRentalPrice")]
        public async Task<IActionResult> GetAvgMonthlyRentalPrice(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAvgMonthlyRentalPriceQuery(), cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<GetAvgMonthlyRentalPriceQueryResult> errorDataResult)
                {
                    if (errorDataResult.ErrorType == "SystemError")
                    {
                        return StatusCode(500, errorDataResult.Message);
                    }
                }
            }
            return Ok(result.Data);
        }

        [HttpGet("GetAutomaticTransmissionCarsCount")]
        public async Task<IActionResult> GetAutomaticTransmissionCarsCount(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAutomaticTransmissionCarsCountQuery(), cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<GetAutomaticTransmissionCarsCountQueryResult> errorDataResult)
                {
                    if (errorDataResult.ErrorType == "SystemError")
                    {
                        return StatusCode(500, errorDataResult.Message);
                    }
                }
            }
            return Ok(result.Data);
        }

        [HttpGet("GetTheBrandWithMostCars")]
        public async Task<IActionResult> GetTheBrandWithMostCars(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetTheBrandWithMostCarsQuery(), cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<GetTheBrandWithMostCarsQueryResult> errorDataResult)
                {
                    if (errorDataResult.ErrorType == "SystemError")
                    {
                        return StatusCode(500, errorDataResult.Message);
                    }
                }
            }
            return Ok(result.Data);
        }

        [HttpGet("GetTheBlogWithTheMostComments")]
        public async Task<IActionResult> GetTheBlogWithTheMostComments(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetTheBlogWithTheMostCommentsQuery(), cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<GetTheBlogWithTheMostCommentsQueryResult> errorDataResult)
                {
                    if (errorDataResult.ErrorType == "SystemError")
                    {
                        return StatusCode(500, errorDataResult.Message);
                    }
                }
            }
            return Ok(result.Data);
        }

        [HttpGet("GetCarsCountWithLessThan1000Km")]
        public async Task<IActionResult> GetCarsCountWithLessThan1000Km(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCarsCountWithLessThan1000KmQuery(), cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<GetCarsCountWithLessThan1000KmQueryResult> errorDataResult)
                {
                    if (errorDataResult.ErrorType == "SystemError")
                    {
                        return StatusCode(500, errorDataResult.Message);
                    }
                }
            }
            return Ok(result.Data);
        }

        [HttpGet("GetGasolineOrDieselCarsCount")]
        public async Task<IActionResult> GetGasolineOrDieselCarsCount(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetGasolineOrDieselCarsCountQuery(), cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<GetGasolineOrDieselCarsCountQueryResult> errorDataResult)
                {
                    if (errorDataResult.ErrorType == "SystemError")
                    {
                        return StatusCode(500, errorDataResult.Message);
                    }
                }
            }
            return Ok(result.Data);
        }

        [HttpGet("GetElectricCarsCount")]
        public async Task<IActionResult> GetElectricCarsCount(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetElectricCarsCountQuery(), cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<GetElectricCarsCountQueryResult> errorDataResult)
                {
                    if (errorDataResult.ErrorType == "SystemError")
                    {
                        return StatusCode(500, errorDataResult.Message);
                    }
                }
            }
            return Ok(result.Data);
        }

        [HttpGet("GetTheHighestPricedCarForDailyRental")]
        public async Task<IActionResult> GetTheHighestPricedCarForDailyRental(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetTheHighestPricedCarForDailyRentalQuery(), cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<GetTheHighestPricedCarForDailyRentalQueryResult> errorDataResult)
                {
                    if (errorDataResult.ErrorType == "SystemError")
                    {
                        return StatusCode(500, errorDataResult.Message);
                    }
                }
            }
            return Ok(result.Data);
        }

        [HttpGet("GetTheLowestPricedCarForDailyRental")]
        public async Task<IActionResult> GetTheLowestPricedCarForDailyRental(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetTheLowestPricedCarForDailyRentalQuery(), cancellationToken);
            if (!result.Success)
            {
                if (result is ErrorDataResult<GetTheLowestPricedCarForDailyRentalQueryResult> errorDataResult)
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
