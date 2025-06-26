using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Commands.FeatureCommands;
using CarBook.Application.RepositoryInterfaces;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Handlers.FeatureHandlers
{
    public class CreateFeatureCommandHandler : IRequestHandler<CreateFeatureCommand, IResult>
    {
        private readonly IFeatureRepository _repository;

        public CreateFeatureCommandHandler(IFeatureRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(CreateFeatureCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                await _repository.CreateAsync(cancellationToken, new Feature
                {
                    Name = request.Name
                });
                return new SuccessResult("Feature created successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
            
        }
    }
}
