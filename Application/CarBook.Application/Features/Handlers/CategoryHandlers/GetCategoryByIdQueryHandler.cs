using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Queries.CategoryQueries;
using CarBook.Application.Features.Results.CategoryResults;
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

namespace CarBook.Application.Features.Handlers.CategoryHandlers
{
    public class GetCategoryByIdQueryHandler: IRequestHandler<GetCategoryByIdQuery, IDataResult<GetCategoryByIdQueryResult>>
    {
        private readonly ICategoryRepository _repository;
        public GetCategoryByIdQueryHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IDataResult<GetCategoryByIdQueryResult>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                Category category = await _repository.GetAsync(cancellationToken, category => category.CategoryId == request.Id);
                if (category == null)
                {
                    return new ErrorDataResult<GetCategoryByIdQueryResult>("Category not found", "BadRequest");
                }
                GetCategoryByIdQueryResult getCategoryByIdQueryResult = new GetCategoryByIdQueryResult
                {
                    CategoryId = category.CategoryId,
                    Name = category.Name
                };
                return new SuccessDataResult<GetCategoryByIdQueryResult>(getCategoryByIdQueryResult);
            }
            catch(Exception ex)
            {
                return new ErrorDataResult<GetCategoryByIdQueryResult>(ex.Message, "SystemError");
            }
            
        }
    }
}
