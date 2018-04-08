using FriendOrganizer.Model;
using FriendOrganizerN2.UI.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendOrganizerN2.UI.ViewModel
{
    public class MainViewModel
    {
        private Friend _selectedFriend;
        private IFriendDataService _friendDataService;


        public ObservableCollection<Friend> Friends { get; set; }


        public MainViewModel(IFriendDataService friendDataService)
        {
            Friends = new ObservableCollection<Friend>();
            _friendDataService = friendDataService;
        }

        public void Load()
        {
            var friends = _friendDataService.GetAll();

            Friends.Clear();
            foreach (var friend in friends)
            {
                Friends.Add(friend);
            }
        }


        public Friend SelectedFriend
        {
            get { return _selectedFriend; }
            set { _selectedFriend = value; }
        }

    }
}
