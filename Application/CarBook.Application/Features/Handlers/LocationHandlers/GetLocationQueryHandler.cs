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
    public class GetLocationQueryHandler : IRequestHandler<GetLocationQuery, IDataResult<IEnumerable<GetLocationQueryResult>>>
    {
        private readonly ILocationRepository _repository;

        public GetLocationQueryHandler(ILocationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IDataResult<IEnumerable<GetLocationQueryResult>>> Handle(GetLocationQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                IEnumerable<Location> locations = await _repository.GetAllAsync(cancellationToken);
                
                IEnumerable<GetLocationQueryResult> getLocationByIdQueryResults = locations.Select(location => new GetLocationQueryResult
                {
                    LocationId = location.LocationId,
                    Name = location.Name
                }).ToList();
                return new SuccessDataResult<IEnumerable<GetLocationQueryResult>>(getLocationByIdQueryResults, "Locations retrieved successfully");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<GetLocationQueryResult>>(ex.Message, "SystemError");
            }
            
        }
    }
}
