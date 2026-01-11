using Microsoft.Identity.Client;

namespace Blog.Client.State
{
    public class NotificationState
    {
        public ViewState GetViewState { get; set; } = ViewState.All;
        public event Action? Onchange;
        public enum ViewState
        {
            All,
            Unread,
            Posts,
            Comments
        }
        public void HandleView(ViewState view)
        {
            GetViewState = view;
            Onchange?.Invoke();
        }
    
    }
}
