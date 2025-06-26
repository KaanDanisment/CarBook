using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Results.TestimonialResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Queries.TestimonialQueries
{
    public class GetTestimonialByIdQuery: IRequest<IDataResult<GetTestimonialByIdQueryResult>>
    {
        public int Id { get; set; }

        public GetTestimonialByIdQuery(int id)
        {
            Id = id;
        }
    }
}
