using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Queries.ServiceQueries;
using CarBook.Application.Features.Results.ServiceResult;
using CarBook.Application.RepositoryInterfaces;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Handlers.ServiceHandlers
{
    public class GetServiceQueryHandler: IRequestHandler<GetServiceQuery, IDataResult<IEnumerable<GetServiceQueryResult>>>
    {
        private IServiceRepository _repository;

        public GetServiceQueryHandler(IServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<IDataResult<IEnumerable<GetServiceQueryResult>>> Handle(GetServiceQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                IEnumerable<Service> services = await _repository.GetAllAsync(cancellationToken);
               
                IEnumerable<GetServiceQueryResult> getServiceQueryResults = services.Select(s => new GetServiceQueryResult
                {
                    ServiceId = s.ServiceId,
                    Title = s.Title,
                    Description = s.Description,
                    IconUrl = s.IconUrl
                }).ToList();
                return new SuccessDataResult<IEnumerable<GetServiceQueryResult>>(getServiceQueryResults, "Services retrieved successfully");
            }
            catch(Exception ex)
            {
                return new ErrorDataResult<IEnumerable<GetServiceQueryResult>>(ex.Message, "SystemError");
            }
           
        }
    }
}
