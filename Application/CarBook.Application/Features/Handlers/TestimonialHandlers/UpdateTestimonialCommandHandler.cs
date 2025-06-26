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
    public class UpdateTestimonialCommandHandler : IRequestHandler<UpdateTestimonialCommand, IResult>
    {
        private readonly ITestimonialRepository _repository;

        public UpdateTestimonialCommandHandler(ITestimonialRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(UpdateTestimonialCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                Testimonial testimonial = await _repository.GetByIdAsync(cancellationToken, request.TestimonialId);
                if (testimonial == null)
                {
                    return new ErrorResult("Testimonial not found", "BadRequest");
                }
                testimonial.Name = request.Name;
                testimonial.Title = request.Title;
                testimonial.Comment = request.Comment;
                testimonial.ImageUrl = request.ImageUrl;
                await _repository.UpdateAsync(cancellationToken, testimonial);
                return new SuccessResult("Testimonial updated successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
            
        }
    }
}
