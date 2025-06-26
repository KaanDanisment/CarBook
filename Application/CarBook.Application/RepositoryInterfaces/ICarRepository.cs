using CarBook.Application.Dtos.CarDtos;
using CarBook.Application.RepositoryInterfaces.GenericRepository;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.RepositoryInterfaces
{
    public interface ICarRepository : IGenericRepository<Car>
    {
        Task<IEnumerable<Car>> GetLatest5Cars(CancellationToken cancellationToken);
        Task<IEnumerable<AvailableCarByLocationDto>> GetAvailableCarsByLocation(CancellationToken cancellationToken, int locationId);
    }
}
