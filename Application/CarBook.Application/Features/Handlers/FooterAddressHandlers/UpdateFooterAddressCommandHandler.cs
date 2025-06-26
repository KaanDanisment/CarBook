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
    public class UpdateFooterAddressCommandHandler: IRequestHandler<UpdateFooterAddressCommand, IResult>
    {
        private readonly IFooterAddressRepository _repository;

        public UpdateFooterAddressCommandHandler(IFooterAddressRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(UpdateFooterAddressCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                FooterAddress footerAddress = await _repository.GetByIdAsync(cancellationToken, request.FooterAddressId);
                if (footerAddress == null)
                {
                    return new ErrorResult("Footer Address not found", "BadRequest");
                }
                footerAddress.Description = request.Description;
                footerAddress.Address = request.Address;
                footerAddress.Phone = request.Phone;
                footerAddress.Email = request.Email;
                await _repository.UpdateAsync(cancellationToken, footerAddress);
                return new SuccessResult("Footer Address updated successfully");
            }
            catch(Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
            
        }
    }
}
