using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Queries.AboutQueries;
using CarBook.Application.Features.Results.AboutResults;
using CarBook.Application.RepositoryInterfaces;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Handlers.AboutHandlers
{
    public class GetAboutByIdQueryHandler : IRequestHandler<GetAboutByIdQuery, IDataResult<GetAboutByIdQueryResult>>
    {
        private readonly IAboutRepository _repository;

        public GetAboutByIdQueryHandler(IAboutRepository repository)
        {
            _repository = repository;
        }

        public async Task<IDataResult<GetAboutByIdQueryResult>> Handle(GetAboutByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                About about = await _repository.GetByIdAsync(cancellationToken, request.Id);
                if(about == null)
                {
                    return new ErrorDataResult<GetAboutByIdQueryResult>("About not found", "BadRequest");
                }

                GetAboutByIdQueryResult getAboutByIdQueryResult = new GetAboutByIdQueryResult
                {
                    AboutId = about.AboutId,
                    Title = about.Title,
                    Description = about.Description,
                    ImageUrl = about.ImageUrl
                };

                return new SuccessDataResult<GetAboutByIdQueryResult>(getAboutByIdQueryResult);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<GetAboutByIdQueryResult>(ex.Message, "SystemError");
            }
        }
    }
}
