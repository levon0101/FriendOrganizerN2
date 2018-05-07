using System.Collections.Generic;
using FriendOrganizer.Model;

namespace FriendOrganizerN2.UI.Data.Repositories
{
    public interface IFriendRepository:IGenericRepository<Friend>
    {       
        void RemovePhoneNumber(FriendPhoneNumber model);
    }
}