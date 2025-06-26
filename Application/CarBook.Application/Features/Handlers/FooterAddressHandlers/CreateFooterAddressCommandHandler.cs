using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Commands.FooterAddressCommands;
using CarBook.Application.RepositoryInterfaces;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Handlers.FooterAddressHandlers
{
    public class CreateFooterAddressCommandHandler : IRequestHandler<CreateFooterAddressCommand, IResult>
    {
        private readonly IFooterAddressRepository _repository;

        public CreateFooterAddressCommandHandler(IFooterAddressRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(CreateFooterAddressCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                await _repository.CreateAsync(cancellationToken, new FooterAddress
                {
                    Description = request.Description,
                    Address = request.Address,
                    Phone = request.Phone,
                    Email = request.Email
                });
                return new SuccessResult("Footer Address created successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message,"SystemError");
            }
            
        }
    }
}
