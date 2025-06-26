using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Queries.AuthorQueries;
using CarBook.Application.Features.Results.AuthorResult;
using CarBook.Application.RepositoryInterfaces;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Handlers.AuthorHandlers
{
    public class GetAuthorQueryHandler : IRequestHandler<GetAuthorQuery, IDataResult<IEnumerable<GetAuthorQueryResult>>>
    {
        private readonly IAuthorRepository _repository;
        public GetAuthorQueryHandler(IAuthorRepository repository)
        {
            _repository = repository;
        }

        public async Task<IDataResult<IEnumerable<GetAuthorQueryResult>>> Handle(GetAuthorQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                IEnumerable<Author> authors = await _repository.GetAllAsync(cancellationToken);
               
                IEnumerable<GetAuthorQueryResult> getAuthorQueryResults = authors.Select(author => new GetAuthorQueryResult
                {
                    AuthorId = author.AuthorId,
                    Name = author.Name,
                    ImageUrl = author.ImageUrl,
                    Description = author.Description
                }).ToList();

                return new SuccessDataResult<IEnumerable<GetAuthorQueryResult>>(getAuthorQueryResults);
            }
            catch(Exception ex)
            {
                return new ErrorDataResult<IEnumerable<GetAuthorQueryResult>>(ex.Message, "SystemError");
            }
        }
    }
}
