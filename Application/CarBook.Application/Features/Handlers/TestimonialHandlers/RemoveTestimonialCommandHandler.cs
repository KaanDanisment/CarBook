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
    public class RemoveTestimonialCommandHandler : IRequestHandler<RemoveTestimonialCommand, IResult>
    {
        private readonly ITestimonialRepository _repository;

        public RemoveTestimonialCommandHandler(ITestimonialRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(RemoveTestimonialCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                Testimonial testimonial = await _repository.GetByIdAsync(cancellationToken, request.TestimonialId);
                if (testimonial == null)
                {
                    return new ErrorResult("Testimonial not found", "BadRequest");
                }
                await _repository.RemoveAsync(cancellationToken, testimonial);
                return new SuccessResult("Testimonial removed successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
           
        }
    }
}
