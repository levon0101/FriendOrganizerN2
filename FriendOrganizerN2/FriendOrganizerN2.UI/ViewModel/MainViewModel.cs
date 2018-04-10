using FriendOrganizer.Model;
using FriendOrganizerN2.UI.Data;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FriendOrganizerN2.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private Friend _selectedFriend;
        private IFriendDataService _friendDataService;

        public ObservableCollection<Friend> Friends { get; set; }


        public MainViewModel(IFriendDataService friendDataService)
        {
            Friends = new ObservableCollection<Friend>();
            _friendDataService = friendDataService;
        }

        public async Task LoadAsync()
        {
            var friends = await _friendDataService.GetAllAsync();

            Friends.Clear();
            foreach (var friend in friends)
            {
                Friends.Add(friend);
            }
        }


        public Friend SelectedFriend
        {
            get { return _selectedFriend; }
            set
            {
                _selectedFriend = value;
                OnPropertyChanged();
            }
        }

    }
}
