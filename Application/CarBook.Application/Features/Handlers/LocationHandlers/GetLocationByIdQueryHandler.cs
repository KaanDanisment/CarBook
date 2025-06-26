using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Queries.LocationQueries;
using CarBook.Application.Features.Results.LocationResult;
using CarBook.Application.RepositoryInterfaces;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Handlers.LocationHandlers
{
    public class GetLocationByIdQueryHandler : IRequestHandler<GetLocationByIdQuery, IDataResult<GetLocationByIdQueryResult>>
    {
        private readonly ILocationRepository _repository;

        public GetLocationByIdQueryHandler(ILocationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IDataResult<GetLocationByIdQueryResult>> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                Location location = await _repository.GetByIdAsync(cancellationToken, request.Id);
                if(location == null)
                {
                    return new ErrorDataResult<GetLocationByIdQueryResult>("Location not found", "BadRequest");
                }
                GetLocationByIdQueryResult getLocationByIdQueryResult = new GetLocationByIdQueryResult
                {
                    LocationId = location.LocationId,
                    Name = location.Name
                };
                return new SuccessDataResult<GetLocationByIdQueryResult>(getLocationByIdQueryResult, "Location retrieved successfully");
            }
            catch(Exception ex)
            {
                return new ErrorDataResult<GetLocationByIdQueryResult>(ex.Message, "SystemError");
            }
            
        }
    }
}
