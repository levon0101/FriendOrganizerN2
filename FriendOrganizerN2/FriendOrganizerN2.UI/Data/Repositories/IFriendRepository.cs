using System.Threading.Tasks;
using FriendOrganizer.Model;

namespace FriendOrganizerN2.UI.Data.Repositories
{
    public interface IFriendRepository:IGenericRepository<Friend>
    {       
        void RemovePhoneNumber(FriendPhoneNumber model);
        Task<bool> HasMeetingAync(int friendId);
    }
}