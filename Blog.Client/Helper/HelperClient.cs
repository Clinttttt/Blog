using BlogApi.Domain.Enums;

namespace Blog.Client.Helper
{
    public static class HelperClient
    {
        public static string GetTimeAgo(DateTime date)
        {
            var timeSpan = DateTime.Now - date;

            if (timeSpan.TotalMinutes < 1) return "just now";
            if (timeSpan.TotalMinutes < 60) return $"{(int)timeSpan.TotalMinutes}m ago";
            if (timeSpan.TotalHours < 24) return $"{(int)timeSpan.TotalHours}h ago";
            if (timeSpan.TotalDays < 7) return $"{(int)timeSpan.TotalDays}d ago";

            return date.ToString("MMM dd");
        }

        public static string ContextAsync(EntityEnum.Type type)
        {
            return type switch
            {
                EntityEnum.Type.Comment => "commented on your post",
                EntityEnum.Type.LikePost => "liked your post",
                EntityEnum.Type.BookMark => "bookmarked your post",
                EntityEnum.Type.LikeComment => "liked your comment",
                _ => "interacted with your content"
            };
        }
        public static (string bg, string Path, string ShadowColor) GetIconConfig(EntityEnum.Type type)
        {
            return type switch
            {
                EntityEnum.Type.Comment => ("bg-indigo-50",
                    "M8 12h.01M12 12h.01M16 12h.01M21 12c0 4.418-4.03 8-9 8a9.863 9.863 0 01-4.255-.949L3 20l1.395-3.72C3.512 15.042 3 13.574 3 12c0-4.418 4.03-8 9-8s9 3.582 9 8z",
                    "indigo-500/20"
                ),
                EntityEnum.Type.LikePost => ("bg-indigo-50",
                    "M4.318 6.318a4.5 4.5 0 000 6.364L12 20.364l7.682-7.682a4.5 4.5 0 00-6.364-6.364L12 7.636l-1.318-1.318a4.5 4.5 0 00-6.364 0z",
                    "rose-500/20"
                ),
                EntityEnum.Type.BookMark => ("bg-indigo-50",
                    "M5 5a2 2 0 012-2h10a2 2 0 012 2v16l-7-3.5L5 21V5z",
                    "amber-500/20"
                ),
                EntityEnum.Type.LikeComment => ("bg-indigo-50",
                    "M14 10h4.764a2 2 0 011.789 2.894l-3.5 7A2 2 0 0115.263 21h-4.017c-.163 0-.326-.02-.485-.06L7 20m7-10V5a2 2 0 00-2-2h-.095c-.5 0-.905.405-.905.905 0 .714-.211 1.412-.608 2.006L7 11v9m7-10h-2M7 20H5a2 2 0 01-2-2v-6a2 2 0 012-2h2.5",
                    "emerald-500/20"
                ),
                _ => ("bg-indigo-200", "M13 16h-1v-4h-1m1-4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z", "gray-500/20")
            };
        }
    }
}
