using FriendOrganizer.Model;
using FriendOrganizerN2.UI.Data;
using FriendOrganizerN2.UI.Data.Lookups;
using FriendOrganizerN2.UI.Event;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FriendOrganizerN2.UI.ViewModel
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private IFriendLookupDataService _friendLookupDataService;
        private IEventAggregator _eventAggregator;

        public ObservableCollection<NavigationItemViewModel> Friends { get; }

        public NavigationViewModel(IFriendLookupDataService friendLookupDataService,
            IEventAggregator eventAggregator)
        {
            _friendLookupDataService = friendLookupDataService;
            _eventAggregator = eventAggregator;

            Friends = new ObservableCollection<NavigationItemViewModel>();

            _eventAggregator.GetEvent<AfterDetailSavedEvent>().Subscribe(AfterDetailSaved);
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>().Subscribe(AfterDetaildDeleted);
        }

        public async Task LoadAsync()
        {
            var lookup = await _friendLookupDataService.GetFreindLookupAsync();
            Friends.Clear();
            foreach (var item in lookup)
            {
                Friends.Add(new NavigationItemViewModel(item.Id, item.DisplayMember,
                    nameof(FriendDetailViewModel), _eventAggregator));
            }
        }


        private void AfterDetaildDeleted(AfterDetailDeletedEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(FriendDetailViewModel):
                    var friend = Friends.Single(f => f.Id == args.Id);
                    if (friend != null)
                    {
                        Friends.Remove(friend);
                    }
                    break;
            }

        }

        private void AfterDetailSaved(AfterDetailSavedEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(FriendDetailViewModel):
                    var lookupItem = Friends.SingleOrDefault(l => l.Id == args.Id);
                    if (lookupItem == null)
                    {
                        Friends.Add(new NavigationItemViewModel(args.Id, args.DisplayMember,
                            nameof(FriendDetailViewModel), _eventAggregator));
                    }
                    else
                    {
                        lookupItem.DisplayMember = args.DisplayMember;
                    }
                    break;
            }
        }

    }
}
