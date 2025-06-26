using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Application.RepositoryInterfaces;
using CarBook.Application.Services.Abstract;
using CarBook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Services.Concrete
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly ILogger<RentalService> _logger;

        public RentalService(IRentalRepository rentalRepository, ILogger<RentalService> logger)
        {
            _rentalRepository = rentalRepository;
            _logger = logger;
        }

        public async Task<IDataResult<IEnumerable<Rental>>> GetRentalsWithCar(CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                DateOnly today = DateOnly.FromDateTime(DateTime.Today);
                DateOnly yesterday = today.AddDays(-1);
                IEnumerable<Rental> rentals = await _rentalRepository.GetAllAsync(cancellationToken, 
                    rental => rental.ReturnDate == yesterday, 
                    include: rental => rental.Include(rental => rental.Car));

                return new SuccessDataResult<IEnumerable<Rental>>(rentals);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving rentals with car");
                return new ErrorDataResult<IEnumerable<Rental>>(ex.Message, "SystemError");
            }
        }
    }
}
