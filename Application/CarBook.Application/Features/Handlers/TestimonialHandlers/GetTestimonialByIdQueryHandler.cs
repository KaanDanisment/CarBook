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
    public class GetTestimonialByIdQueryHandler : IRequestHandler<GetTestimonialByIdQuery, IDataResult<GetTestimonialByIdQueryResult>>
    {
        private readonly ITestimonialRepository _repository;

        public GetTestimonialByIdQueryHandler(ITestimonialRepository repository)
        {
            _repository = repository;
        }

        public async Task<IDataResult<GetTestimonialByIdQueryResult>> Handle(GetTestimonialByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                Testimonial testimonial = await _repository.GetByIdAsync(cancellationToken, request.Id);
                if(testimonial == null)
                {
                    return new ErrorDataResult<GetTestimonialByIdQueryResult>("Testimonial not found", "BadRequest");
                }
                GetTestimonialByIdQueryResult getTestimonialByIdQueryResult = new GetTestimonialByIdQueryResult
                {
                    TestimonialId = testimonial.TestimonialId,
                    Name = testimonial.Name,
                    Title = testimonial.Title,
                    Comment = testimonial.Comment,
                    ImageUrl = testimonial.ImageUrl
                };
                return new SuccessDataResult<GetTestimonialByIdQueryResult>(getTestimonialByIdQueryResult, "Testimonial found successfully");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<GetTestimonialByIdQueryResult>(ex.Message, "SystemError");
            }
        }
    }
}
