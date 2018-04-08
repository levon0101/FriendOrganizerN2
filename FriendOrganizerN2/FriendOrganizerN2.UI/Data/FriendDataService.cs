using FriendOrganizer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendOrganizerN2.UI.Data
{
    public class FriendDataService : IFriendDataService
    {

        //using Named Interator for organize data service
        public IEnumerable<Friend> GetAll()
        {
            // To Do
            yield return new Friend { FirstName = "Rafael", LastName = "Grigoryan", Email = "--" };
            yield return new Friend { FirstName = "Karen", LastName = "Barseghyan", Email = "--" };
            yield return new Friend { FirstName = "Stive", LastName = "Voznyak", Email = "--" };
            yield return new Friend { FirstName = "Gelelo", LastName = "Galelei", Email = "--" };
        }
    }
}
