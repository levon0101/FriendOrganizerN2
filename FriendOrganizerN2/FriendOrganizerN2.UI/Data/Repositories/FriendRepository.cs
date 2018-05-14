using FirendOrganizerN2.DataAcces;
using FriendOrganizer.Model;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace FriendOrganizerN2.UI.Data.Repositories
{
    public class FriendRepository : GenericRepository<Friend, FriendOrganizerDbContext>,
                                    IFriendRepository
    {
        public FriendRepository(FriendOrganizerDbContext context) : base(context)
        {
        }

        //using Named Interator for organize data service
        public override async Task<Friend> GetByIdAsync(int friendId)
        {
            return await Context.Friends
                .Include(f => f.PhoneNumbers)
                .SingleAsync(f => f.Id == friendId);

        }

        public Task<bool> HasMeetingAync(int friendId)
        {
            return Context.Meetings.AsNoTracking()
                .Include(m => m.Friends)
                .AnyAsync(m => m.Friends.Any(f => f.Id == friendId));
        }
        
        public void RemovePhoneNumber(FriendPhoneNumber model)
        {
            Context.FriendPhoneNumbers.Remove(model);
        } 
    }
}

