using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Queries.BrandQueries;
using CarBook.Application.Features.Results.BrandResults;
using CarBook.Application.RepositoryInterfaces;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Handlers.BrandHandlers
{
    public class GetBrandByIdQueryHandler: IRequestHandler<GetBrandByIdQuery, IDataResult<GetBrandByIdQueryResult>>
    {
        private readonly IBrandRepository _repository;
        public GetBrandByIdQueryHandler(IBrandRepository repository)
        {
            _repository = repository;
        }

        public async Task<IDataResult<GetBrandByIdQueryResult>> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Brand brand = await _repository.GetByIdAsync(cancellationToken, request.Id);
                if (brand == null)
                {
                    return new ErrorDataResult<GetBrandByIdQueryResult>("Brand not found", "BadRequest");
                }
                GetBrandByIdQueryResult getBrandByIdQueryResult = new GetBrandByIdQueryResult
                {
                    BrandId = brand.BrandId,
                    Name = brand.Name
                };
                return new SuccessDataResult<GetBrandByIdQueryResult>(getBrandByIdQueryResult);
            }
            catch(Exception ex)
            {
                return new ErrorDataResult<GetBrandByIdQueryResult>(ex.Message, "SystemError");
            }
            
        }
    }
}
