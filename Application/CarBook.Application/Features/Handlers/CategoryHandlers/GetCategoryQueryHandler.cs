using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Queries.CategoryQueries;
using CarBook.Application.Features.Results.CategoryResults;
using CarBook.Application.RepositoryInterfaces;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Handlers.CategoryHandlers
{
    public class GetCategoryQueryHandler: IRequestHandler<GetCategoryQuery, IDataResult<IEnumerable<GetCategoryQueryResult>>>
    {
        private readonly ICategoryRepository _repository;
        public GetCategoryQueryHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IDataResult<IEnumerable<GetCategoryQueryResult>>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                IEnumerable<Category> categories = await _repository.GetAllAsync(cancellationToken);
                
                IEnumerable<GetCategoryQueryResult> getCategoryQueryResults = categories.Select(c => new GetCategoryQueryResult
                {
                    CategoryId = c.CategoryId,
                    Name = c.Name
                });
                return new SuccessDataResult<IEnumerable<GetCategoryQueryResult>>(getCategoryQueryResults);
            }
            catch(Exception ex)
            {
                return new ErrorDataResult<IEnumerable<GetCategoryQueryResult>>(ex.Message, "SystemError");
            }
            
        }
    }
}
