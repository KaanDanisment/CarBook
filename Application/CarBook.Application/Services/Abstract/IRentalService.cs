using CarBook.Application.Common.Results.Abstracts;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Services.Abstract
{
    public interface IRentalService
    {
        Task<IDataResult<IEnumerable<Rental>>> GetRentalsWithCar(CancellationToken cancellationToken);
    }
}
