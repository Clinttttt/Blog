using Blog.Application.Queries.Posts.RecentActivity;
using BlogApi.Application.Dtos;
using BlogApi.Application.Models;
using BlogApi.Domain.Common;

namespace BlogApi.Client.Interface
{
    public interface IPostAnalyticsClientService
    {
        Task<Result<StatisticsDto>> GetPublicStatistics();
        Task<Result<StatisticsDto>> GetStatistics();
        Task<Result<List<RecentActivityItemDto>>> GetRecentActivity(int limit = 4, int daysBack = 7);
    }
}