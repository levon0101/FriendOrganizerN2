using FirendOrganizerN2.DataAcces;
using FriendOrganizer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace FriendOrganizerN2.UI.Data
{
    public class FriendDataService : IFriendDataService
    {
        private Func<FriendOrganizerDbContext> _contextCreator;

        public FriendDataService(Func<FriendOrganizerDbContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }

        //using Named Interator for organize data service
        public async Task<List<Friend>> GetAllAsync()
        { 
            using (var ctx = _contextCreator())
            {
                return await ctx.Friends.AsNoTracking().ToListAsync();
            }
        }
    }
}
