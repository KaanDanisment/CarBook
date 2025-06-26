using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Application.Dtos.CustomerDtos;
using CarBook.Application.Features.Commands.RentalCommands;
using CarBook.Application.RepositoryInterfaces;
using CarBook.Application.Services.Abstract;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Handlers.RentalHandlers
{
    public class CreateRentalCommandHandler : IRequestHandler<CreateRentalCommand, IResult>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly ICustomerService _customerService;
        private readonly ITransactionService _transactionService;
        private readonly ICarService _carService;

        public CreateRentalCommandHandler(IRentalRepository rentalRepository, ICustomerService customerService, ITransactionService transactionService, ICarService carService)
        {
            _rentalRepository = rentalRepository;
            _customerService = customerService;
            _transactionService = transactionService;
            _carService = carService;
        }

        public async Task<IResult> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _transactionService.ExecuteAsync(async () =>
                {
                    IDataResult<CustomerDto> customerDto = await _customerService.GetCustomerByEmail(request.CustomerEmail, cancellationToken);
                    if (!customerDto.Success)
                    {
                        throw new Exception(customerDto.Message);
                    }
                    if(customerDto.Data == null)
                    {
                        customerDto = await _customerService.CreateCustomer(new CreateCustomerDto
                        {
                            Name = request.CustomerName,
                            Email = request.CustomerEmail,
                            PhoneNumber = request.CustomerPhoneNumber
                        }, cancellationToken);
                        if (!customerDto.Success)
                        {
                            throw new Exception(customerDto.Message);
                        }
                    }
                    var carLocation = await _carService.UpdateCarLocation(request.CarId, request.DropoffLocationId, cancellationToken);
                    if (!carLocation.Success)
                    {
                        throw new Exception(carLocation.Message);
                    }

                    var carRentalStatus = await _carService.TurnFalseCarAvailability(request.CarId, cancellationToken);
                    if (!carRentalStatus.Success)
                    {
                        throw new Exception(carRentalStatus.Message);
                    }

                    await _rentalRepository.CreateAsync(cancellationToken, new Rental
                    {
                        CarId = request.CarId,
                        CustomerId = customerDto.Data.Id,
                        RentDate = request.RentDate,
                        ReturnDate = request.ReturnDate,
                        RentTime = request.RentTime,
                        ReturnTime = request.ReturnTime,
                        PickupLocationId = request.PickupLocationId,
                        DropoffLocationId = request.DropoffLocationId,
                        TotalPrice = request.TotalPrice
                    });
                    
                }, cancellationToken);

                return new SuccessResult("Rental created successfully");
            }
            catch(Exception ex)
            {
                return new ErrorResult(ex.Message,"SystemError");
            }
        }
    }
}
