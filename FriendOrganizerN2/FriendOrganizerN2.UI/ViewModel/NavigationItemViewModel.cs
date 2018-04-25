using FriendOrganizerN2.UI.Event;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FriendOrganizerN2.UI.ViewModel
{
    public class NavigationItemViewModel : ViewModelBase
    {
        private string _displayMamber;
        private IEventAggregator _eventAggregator;

        public NavigationItemViewModel(int id, string displayMamber,
            IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            Id = id;
            DisplayMember = displayMamber;
            OpenFriendDetailViewCommand = new DelegateCommand(OnOpenFriendDetailView);
        }


        public int Id { get; }
        
        public string DisplayMember
        {
            get { return _displayMamber; }
            set
            {
                _displayMamber = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenFriendDetailViewCommand { get;}
        
        private void OnOpenFriendDetailView()
        {
            _eventAggregator.GetEvent<OpenFriendDetailViewEvent>().Publish(Id);
        }
    }
}
