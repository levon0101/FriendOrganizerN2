using System.Threading.Tasks;

namespace FriendOrganizerN2.UI.ViewModel
{
    public interface IFriendDetailViewModel
    {
        Task LoadAsync(int friendId);
        bool HasChanges { get; }
    }
}