using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Commands.TestimonialCommands;
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
    public class CreateTestimonialCommandHandler : IRequestHandler<CreateTestimonialCommand, IResult>
    {
        private readonly ITestimonialRepository _repository;

        public CreateTestimonialCommandHandler(ITestimonialRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(CreateTestimonialCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                await _repository.CreateAsync(cancellationToken, new Testimonial
                {
                    Name = request.Name,
                    Title = request.Title,
                    Comment = request.Comment,
                    ImageUrl = request.ImageUrl
                });
                return new SuccessResult("Testimonial created successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
            
        }
    }
}
