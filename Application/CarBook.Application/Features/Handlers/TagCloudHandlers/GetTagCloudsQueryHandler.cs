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
    public class GetTagCloudsQueryHandler : IRequestHandler<GetTagCloudQuery, IDataResult<IEnumerable<GetTagCloudQueryResult>>>
    {
        private readonly ITagCloudRepository _repository;

        public GetTagCloudsQueryHandler(ITagCloudRepository repository)
        {
            _repository = repository;
        }

        public async Task<IDataResult<IEnumerable<GetTagCloudQueryResult>>> Handle(GetTagCloudQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                IEnumerable<TagCloud> tagClouds = await _repository.GetAllAsync(cancellationToken);
               
                IEnumerable<GetTagCloudQueryResult> getTagCloudQueryResults = tagClouds.Select(tagCloud => new GetTagCloudQueryResult
                {
                    TagCloudId = tagCloud.TagCloudId,
                    Title = tagCloud.Title,
                    BlogId = tagCloud.BlogId
                });
                return new SuccessDataResult<IEnumerable<GetTagCloudQueryResult>>(getTagCloudQueryResults, "TagClouds found successfully");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<GetTagCloudQueryResult>>(ex.Message, "SystemError");
            }
            
        }
    }
}
