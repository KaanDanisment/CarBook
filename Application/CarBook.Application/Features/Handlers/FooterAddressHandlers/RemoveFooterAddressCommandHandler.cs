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
    public class RemoveFooterAddressCommandHandler : IRequestHandler<RemoveFooterAddressCommand, IResult>
    {
        private readonly IFooterAddressRepository _repository;

        public RemoveFooterAddressCommandHandler(IFooterAddressRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(RemoveFooterAddressCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                FooterAddress footerAddress = await _repository.GetByIdAsync(cancellationToken, request.Id);
                if (footerAddress == null)
                {
                    return new ErrorResult("Footer Address not found", "BadRequest");
                }
                await _repository.RemoveAsync(cancellationToken, footerAddress);
                return new SuccessResult("Footer Address removed successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
           
        }
    }
}
