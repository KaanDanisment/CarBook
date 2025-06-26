using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Queries.TagCloudQueries;
using CarBook.Application.Features.Results.TagCloudResults;
using CarBook.Application.RepositoryInterfaces;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Handlers.TagCloudHandlers
{
    public class GetTagCloudByIdQueryHandler : IRequestHandler<GetTagCloudByIdQuery, IDataResult<GetTagCloudByIdQueryResult>>
    {
        private readonly ITagCloudRepository _repository;

        public GetTagCloudByIdQueryHandler(ITagCloudRepository repository)
        {
            _repository = repository;
        }

        public async Task<IDataResult<GetTagCloudByIdQueryResult>> Handle(GetTagCloudByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                TagCloud tagCloud = await _repository.GetByIdAsync(cancellationToken, request.Id);
                if (tagCloud == null)
                {
                    return new ErrorDataResult<GetTagCloudByIdQueryResult>("TagCloud not found", "BadRequest");
                }
                GetTagCloudByIdQueryResult getTagCloudByIdQueryResult = new GetTagCloudByIdQueryResult
                {
                    TagCloudId = tagCloud.TagCloudId,
                    Title = tagCloud.Title,
                    BlogId = tagCloud.BlogId
                };
                return new SuccessDataResult<GetTagCloudByIdQueryResult>(getTagCloudByIdQueryResult, "TagCloud found successfully");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<GetTagCloudByIdQueryResult>(ex.Message, "SystemError");
            }
            
        }
    }
}
