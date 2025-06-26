using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Queries.BrandQueries;
using CarBook.Application.Features.Results.BrandResults;
using CarBook.Application.RepositoryInterfaces;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Handlers.BrandHandlers
{
    public class GetBrandQueryHandler: IRequestHandler<GetBrandQuery, IDataResult<IEnumerable<GetBrandQueryResult>>>
    {
        private readonly IBrandRepository _repository;
        public GetBrandQueryHandler(IBrandRepository repository)
        {
            _repository = repository;
        }

        public async Task<IDataResult<IEnumerable<GetBrandQueryResult>>> Handle(GetBrandQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                IEnumerable<Brand> brands = await _repository.GetAllAsync(cancellationToken);
               
                IEnumerable<GetBrandQueryResult> getBrandQueryResult = brands.Select(x => new GetBrandQueryResult
                {
                    BrandId = x.BrandId,
                    Name = x.Name
                }).ToList();
                return new SuccessDataResult<IEnumerable<GetBrandQueryResult>>(getBrandQueryResult);
            }
            catch(Exception ex)
            {
                return new ErrorDataResult<IEnumerable<GetBrandQueryResult>>(ex.Message, "SystemError");
            }
            
        }
    }
}
