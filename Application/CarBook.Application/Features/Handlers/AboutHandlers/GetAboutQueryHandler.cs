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
    public class GetAboutQueryHandler : IRequestHandler<GetAboutQuery, IDataResult<IEnumerable<GetAboutQueryResult>>>
    {
        private readonly IAboutRepository _repository;

        public GetAboutQueryHandler(IAboutRepository repository)
        {
            _repository = repository;
        }

        public async Task<IDataResult<IEnumerable<GetAboutQueryResult>>> Handle(GetAboutQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                IEnumerable<About> abouts = await _repository.GetAllAsync(cancellationToken);

                IEnumerable<GetAboutQueryResult> getAboutQueryResults = abouts.Select(about => new GetAboutQueryResult
                {
                    AboutId = about.AboutId,
                    Title = about.Title,
                    Description = about.Description,
                    ImageUrl = about.ImageUrl
                }).ToList();

                return new SuccessDataResult<IEnumerable<GetAboutQueryResult>>(getAboutQueryResults);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<GetAboutQueryResult>>(ex.Message, "SystemError");
            }
        }
    }
}
