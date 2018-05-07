using System.Threading.Tasks;

namespace FriendOrganizerN2.UI.ViewModel
{
    public interface IDetailViewModel
    {
        Task LoadAsync(int? id);
        bool HasChanges { get; }
    }
}