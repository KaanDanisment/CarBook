using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Commands.BlogCommands;
using CarBook.Application.RepositoryInterfaces;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Handlers.BlogHandlers
{
    public class RemoveBlogCommandHandler : IRequestHandler<RemoveBlogCommand, IResult>
    {
        private readonly IBlogRepository _repository;
        public RemoveBlogCommandHandler(IBlogRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(RemoveBlogCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                Blog blog = await _repository.GetByIdAsync(cancellationToken, request.Id);
                if (blog == null)
                {
                    return new ErrorResult("Blog not found", "BadRequest");
                }
                await _repository.RemoveAsync(cancellationToken, blog);
                return new SuccessResult("Blog removed successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
        }
    }
}
