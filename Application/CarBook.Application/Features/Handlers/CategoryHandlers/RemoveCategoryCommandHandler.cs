using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Commands.CategoryCommands;
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
    public class RemoveCategoryCommandHandler: IRequestHandler<RemoveCategoryCommand, IResult>
    {
        private readonly ICategoryRepository _repository;
        public RemoveCategoryCommandHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(RemoveCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                Category category = await _repository.GetByIdAsync(cancellationToken, request.Id);
                if (category == null)
                {
                    return new ErrorResult("Category not found", "BadRequest");
                }
                await _repository.RemoveAsync(cancellationToken, category);
                return new SuccessResult("Category removed successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
            
        }
    }
}
