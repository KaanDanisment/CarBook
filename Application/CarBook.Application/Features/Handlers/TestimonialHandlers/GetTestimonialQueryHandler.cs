using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Queries.TestimonialQueries;
using CarBook.Application.Features.Results.TestimonialResult;
using CarBook.Application.RepositoryInterfaces;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Handlers.TestimonialHandlers
{
    public class GetTestimonialQueryHandler : IRequestHandler<GetTestimonialQuery, IDataResult<IEnumerable<GetTestimonialQueryResult>>>
    {
        private readonly ITestimonialRepository _repository;

        public GetTestimonialQueryHandler(ITestimonialRepository repository)
        {
            _repository = repository;
        }

        public async Task<IDataResult<IEnumerable<GetTestimonialQueryResult>>> Handle(GetTestimonialQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                IEnumerable<Testimonial> testimonials = await _repository.GetAllAsync(cancellationToken);
                
                IEnumerable<GetTestimonialQueryResult> getTestimonialQueryResults = testimonials.Select(t => new GetTestimonialQueryResult
                {
                    TestimonialId = t.TestimonialId,
                    Name = t.Name,
                    Title = t.Title,
                    Comment = t.Comment,
                    ImageUrl = t.ImageUrl
                }).ToList();
                return new SuccessDataResult<IEnumerable<GetTestimonialQueryResult>>(getTestimonialQueryResults, "Testimonials found successfully");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<GetTestimonialQueryResult>>(ex.Message, "SystemError");
            }
            
        }
    }
}
