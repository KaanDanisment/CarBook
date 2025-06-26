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
    public class RemoveBrandCommandHandler: IRequestHandler<RemoveBrandCommand, IResult>
    {
        private readonly IBrandRepository _repository;
        public RemoveBrandCommandHandler(IBrandRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(RemoveBrandCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                Brand brand = await _repository.GetByIdAsync(cancellationToken, request.Id);
                if (brand == null)
                {
                    return new ErrorResult("Brand not found", "BadRequest");
                }
                await _repository.RemoveAsync(cancellationToken, brand);
                return new SuccessResult("Brand removed successfully");
            }
            catch(Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
            
        }
    }
}
