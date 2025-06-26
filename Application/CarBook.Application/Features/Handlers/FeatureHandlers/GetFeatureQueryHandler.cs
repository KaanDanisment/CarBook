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
    public class GetFeatureQueryHandler : IRequestHandler<GetFeatureQuery, IDataResult<IEnumerable<GetFeatureQueryResult>>>
    {
        private readonly IFeatureRepository _repository;

        public GetFeatureQueryHandler(IFeatureRepository repository)
        {
            _repository = repository;
        }

        public async Task<IDataResult<IEnumerable<GetFeatureQueryResult>>> Handle(GetFeatureQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                IEnumerable<Feature> features = await _repository.GetAllAsync(cancellationToken);
                
                IEnumerable<GetFeatureQueryResult> getFeatureQueryResult = features.Select(f => new GetFeatureQueryResult
                {
                    FeatureId = f.FeatureId,
                    Name = f.Name
                });
                return new SuccessDataResult<IEnumerable<GetFeatureQueryResult>>(getFeatureQueryResult);
            }
            catch(Exception ex)
            {
                return new ErrorDataResult<IEnumerable<GetFeatureQueryResult>>(ex.Message, "SystemError");
            }
            
        }
    }
}
