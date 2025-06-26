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
    public class CreateBrandCommandHandler: IRequestHandler<CreateBrandCommand, IResult>
    {
        private readonly IBrandRepository _repository;

        public CreateBrandCommandHandler(IBrandRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                await _repository.CreateAsync(cancellationToken, new Brand
                {
                    Name = request.Name
                });
                return new SuccessResult("Brand created successfully");
            }
            catch(Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
        }
    }
}
