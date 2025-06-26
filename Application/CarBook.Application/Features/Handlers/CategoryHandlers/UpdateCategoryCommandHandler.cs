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
    public class UpdateCategoryCommandHandler: IRequestHandler<UpdateCategoryCommand, IResult>
    {
        private readonly ICategoryRepository _repository;
        public UpdateCategoryCommandHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                Category category = await _repository.GetByIdAsync(cancellationToken, request.CategoryId);
                if(category == null)
                {
                    return new ErrorResult("Category not found", "BadRequest");
                }
                category.Name = request.Name;
                await _repository.UpdateAsync(cancellationToken, category);
                return new SuccessResult("Category updated successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
           
        }
    }
}
