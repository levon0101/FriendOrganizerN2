
using System.Threading.Tasks;

namespace FriendOrganizerN2.UI.View.Services
{
    public interface IMessageDialogService
    {
        Task<MessageDialogResult> ShowOkCancelDialogAsync(string text, string title);
        Task ShowInfoAsync(string text);
    }
}