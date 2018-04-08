using System.Collections.Generic;
using FriendOrganizer.Model;

namespace FriendOrganizerN2.UI.Data
{
    public interface IFriendDataService
    {
        IEnumerable<Friend> GetAll();
    }
}