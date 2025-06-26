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
    public class RemoveFeatureCommandHandler : IRequestHandler<RemoveFeatureCommand, IResult>
    {
        private readonly IFeatureRepository _repository;

        public RemoveFeatureCommandHandler(IFeatureRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(RemoveFeatureCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                Feature feature = await _repository.GetByIdAsync(cancellationToken, request.Id);
                if (feature == null)
                {
                    return new ErrorResult("Feature not found", "BadRequest");
                }
                await _repository.RemoveAsync(cancellationToken, feature);
                return new SuccessResult("Feature removed successfully");
            }
            catch(Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
            
        }
    }
}
