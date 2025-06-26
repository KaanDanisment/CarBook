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
    public class GetServiceByIdQueryHandler : IRequestHandler<GetServiceByIdQuery, IDataResult<GetServiceByIdQueryResult>>
    {
        private readonly IServiceRepository _repository;

        public GetServiceByIdQueryHandler(IServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<IDataResult<GetServiceByIdQueryResult>> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                Service service = await _repository.GetByIdAsync(cancellationToken, request.Id);
                if(service == null)
                {
                    return new ErrorDataResult<GetServiceByIdQueryResult>("Service not found", "BadRequest");
                }
                GetServiceByIdQueryResult getServiceByIdQueryResult = new GetServiceByIdQueryResult
                {
                    ServiceId = service.ServiceId,
                    Title = service.Title,
                    Description = service.Description,
                    IconUrl = service.IconUrl
                };
                return new SuccessDataResult<GetServiceByIdQueryResult>(getServiceByIdQueryResult, "Service retrieved successfully");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<GetServiceByIdQueryResult>(ex.Message, "SystemError");
            }
            
        }
    }
}
