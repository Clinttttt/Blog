using BlogApi.Application.Common.Interfaces;
using BlogApi.Application.Dtos;
using BlogApi.Application.Models;
using BlogApi.Application.Queries.Posts;
using BlogApi.Domain.Common;
using BlogApi.Domain.Entities;
using MediatR;
using System.Linq.Expressions;
using static BlogApi.Domain.Enums.EntityEnum;

public class GetPagedPostsQueryHandler : IRequestHandler<GetPagedPostsQuery, Result<PagedResult<PostDto>>>
{
    private readonly IPostRespository _repository;

    public GetPagedPostsQueryHandler(IPostRespository repository)
    {
        _repository = repository;
    }

    public async Task<Result<PagedResult<PostDto>>> Handle(GetPagedPostsQuery request, CancellationToken cancellationToken)
    {
        var filter = BuildFilter(request);

        var postPage = await _repository.GetPaginatedPostDtoAsync(
            request.UserId,
            request.PageNumber,
            request.PageSize,
            filter: filter,
            cancellationToken);

        if (!postPage.Value!.Items.Any())
            return Result<PagedResult<PostDto>>.NoContent();

        return Result<PagedResult<PostDto>>.Success(postPage.Value);
    }

    private Expression<Func<Post, bool>>? BuildFilter(GetPagedPostsQuery request)
    {
        return request.FilterType switch
        {
            PostFilterType.Published =>
                p => p.Status == Status.Published,

            PostFilterType.PublishedByUser =>
                p => p.UserId == request.UserId && p.Status == Status.Published,

            PostFilterType.Drafts =>
                p => p.Status == Status.Draft,

            PostFilterType.DraftsByUser =>
                p => p.UserId == request.UserId && p.Status == Status.Draft,

            PostFilterType.Pending =>
                p => p.Status == Status.Pending,

            PostFilterType.PendingByUser =>
                p => p.UserId == request.UserId && p.Status == Status.Pending,

            PostFilterType.ByCategory =>
                p => p.CategoryId == request.CategoryId,

            PostFilterType.ByTag =>
                p => p.PostTags.Any(pt => pt.TagId == request.TagId),

            _ => null
        };
    }
}
