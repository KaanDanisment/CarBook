using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Commands.BrandCommands;
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
    public class UpdateBrandCommandHandler: IRequestHandler<UpdateBrandCommand, IResult>
    {
        private readonly IBrandRepository _repository;
        public UpdateBrandCommandHandler(IBrandRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                Brand brand = await _repository.GetByIdAsync(cancellationToken, request.BrandId);
                if(brand == null)
                {
                    return new ErrorResult("Brand not found", "BadRequest");
                }
                brand.Name = request.Name;
                await _repository.UpdateAsync(cancellationToken, brand);
                return new SuccessResult("Brand updated successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
        }
    }
}
