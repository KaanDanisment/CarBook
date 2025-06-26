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
    public class GetAuthorByIdQueryHandler: IRequestHandler<GetAuthorByIdQuery, IDataResult<GetAuthorByIdQueryResult>>
    {
        private readonly IAuthorRepository _repository;

        public GetAuthorByIdQueryHandler(IAuthorRepository repository)
        {
            _repository = repository;
        }

        public async Task<IDataResult<GetAuthorByIdQueryResult>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                Author author = await _repository.GetByIdAsync(cancellationToken, request.Id);
                if (author == null)
                {
                    return new ErrorDataResult<GetAuthorByIdQueryResult>("Author Not Found", "BadRequest");
                }
                GetAuthorByIdQueryResult getAuthorByIdQueryResult = new GetAuthorByIdQueryResult
                {
                    AuthorId = author.AuthorId,
                    Name = author.Name,
                    ImageUrl = author.ImageUrl,
                    Description = author.Description
                };
                return new SuccessDataResult<GetAuthorByIdQueryResult>(getAuthorByIdQueryResult);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<GetAuthorByIdQueryResult>(ex.Message, "SystemError");
            }
        }
    }
}
