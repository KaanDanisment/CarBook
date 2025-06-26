using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Queries.FeatureQueries;
using CarBook.Application.Features.Results.FeatureResults;
using CarBook.Application.RepositoryInterfaces;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Handlers.FeatureHandlers
{
    public class GetFeatureByIdQueryHandler: IRequestHandler<GetFeatureByIdQuery,IDataResult<GetFeatureByIdQueryResult>>
    {
        private readonly IFeatureRepository _repository;

        public GetFeatureByIdQueryHandler(IFeatureRepository repository)
        {
            _repository = repository;
        }

        public async Task<IDataResult<GetFeatureByIdQueryResult>> Handle(GetFeatureByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                Feature feature = await _repository.GetByIdAsync(cancellationToken, request.Id);
                if(feature == null)
                {
                    return new ErrorDataResult<GetFeatureByIdQueryResult>("Feature not found", "BadRequest");
                }
                GetFeatureByIdQueryResult getFeatureByIdQueryResult = new GetFeatureByIdQueryResult
                {
                    FeatureId = feature.FeatureId,
                    Name = feature.Name
                };
                return new SuccessDataResult<GetFeatureByIdQueryResult>(getFeatureByIdQueryResult);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<GetFeatureByIdQueryResult>(ex.Message,"SystemError");
            }
            
        }
    }
}
