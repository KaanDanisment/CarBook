using CarBook.Application.Common.Results.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Services.Abstract
{
    public interface ICarService
    {
        Task<IResult> TurnFalseCarAvailability(int carId, CancellationToken cancellationToken);
        Task UpdateCarAvailability();
        Task<IResult> UpdateCarLocation(int carId, int locationId, CancellationToken cancellationToken);
    }
}
