using Prism.Events;

namespace FriendOrganizerN2.UI.Event
{
    public class OpenDetailViewEvent : PubSubEvent<OpenDetailViewEventArgs>
    {
    }
    public class OpenDetailViewEventArgs
    {
        public int? Id { get; set; }
        public string ViewModelName { get; set; }
    }
}
