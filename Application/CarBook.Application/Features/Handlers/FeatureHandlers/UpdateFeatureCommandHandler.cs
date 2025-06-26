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
    public class UpdateFeatureCommandHandler : IRequestHandler<UpdateFeatureCommand, IResult>
    {
        private readonly IFeatureRepository _repository;

        public UpdateFeatureCommandHandler(IFeatureRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(UpdateFeatureCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                Feature feature = await _repository.GetByIdAsync(cancellationToken, request.FeatureId);
                if (feature == null)
                {
                    return new ErrorResult("Feature not found", "BadRequest");
                }
                feature.Name = request.Name;
                await _repository.UpdateAsync(cancellationToken, feature);
                return new SuccessResult("Feature updated successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
            
        }
    }
}
