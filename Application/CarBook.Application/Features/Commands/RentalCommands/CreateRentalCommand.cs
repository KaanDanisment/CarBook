using CarBook.Application.Common.Results.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Commands.RentalCommands
{
    public class CreateRentalCommand: IRequest<IResult>
    {
        public int CarId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public DateOnly RentDate { get; set; }
        public DateOnly? ReturnDate { get; set; }
        public TimeOnly? RentTime { get; set; }
        public TimeOnly? ReturnTime { get; set; }
        public int PickupLocationId { get; set; }
        public int DropoffLocationId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
