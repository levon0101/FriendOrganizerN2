﻿using System.Collections.Generic;
using System.Threading.Tasks;
using FriendOrganizer.Model;

namespace FriendOrganizerN2.UI.Data
{
    public interface IFriendDataService
    {
        Task<Friend> GetByIdAsync(int friendId);
    }
}