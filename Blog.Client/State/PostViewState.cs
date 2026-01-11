using Blog.Client.Components.Pages.Public;

namespace Blog.Client.State
{
    public class PostViewState
    {
        public event Action? Onchange;

        public AuthorPostState AuthorPageView { get; set; } = AuthorPostState.all_post;
        public AdminPostState AdminPageView { get; set; } = AdminPostState.home;
        public enum AuthorPostState
        {
            all_post,
            published,
            pending,
            drafts,
            bookmarks
        }
        public enum AdminPostState
        {
            home,
            all_post,
            published,
            draft,
            author,
            overview
        }
        public void AuthorView(AuthorPostState view)
        {
            AuthorPageView = view;
            Onchange?.Invoke();
        }
        public void AdminView(AdminPostState view)
        {
            AdminPageView = view;
            Onchange?.Invoke();
        }
    
    }
}
