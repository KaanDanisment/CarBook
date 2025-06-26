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
    public class GetTagCloudByBlogIdQueryHandler : IRequestHandler<GetTagCloudByBlogIdQuery, IDataResult<IEnumerable<GetTagCloudByBlogIdQueryResult>>>
    {
        private readonly ITagCloudRepository _tagCloudRepository;

        public GetTagCloudByBlogIdQueryHandler(ITagCloudRepository tagCloudRepository)
        {
            _tagCloudRepository = tagCloudRepository;
        }

        public async Task<IDataResult<IEnumerable<GetTagCloudByBlogIdQueryResult>>> Handle(GetTagCloudByBlogIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                IEnumerable<TagCloud> tagClouds = await _tagCloudRepository.GetAllAsync(cancellationToken, tagCloud => tagCloud.TagCloudId == request.BlogId);
                if(tagClouds == null || !tagClouds.Any())
                {
                    return new ErrorDataResult<IEnumerable<GetTagCloudByBlogIdQueryResult>>("TagClouds not found", "BadRequest");
                }
                IEnumerable<GetTagCloudByBlogIdQueryResult> getTagCloudByBlogIdQueryResults = tagClouds.Select(tagCloud => new GetTagCloudByBlogIdQueryResult
                {
                    TagCloudId = tagCloud.TagCloudId,
                    Title = tagCloud.Title,
                    BlogId = tagCloud.BlogId
                });
                return new SuccessDataResult<IEnumerable<GetTagCloudByBlogIdQueryResult>>(getTagCloudByBlogIdQueryResults, "TagClouds found successfully");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<GetTagCloudByBlogIdQueryResult>>(ex.Message, "SystemError");
            }
            
        }
    }
}
