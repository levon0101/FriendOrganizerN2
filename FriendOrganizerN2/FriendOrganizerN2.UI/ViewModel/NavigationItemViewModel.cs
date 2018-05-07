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
        private string _detailViewModelName;

        public NavigationItemViewModel(int id, string displayMamber,
            string detailViewModelName,
            IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _detailViewModelName = detailViewModelName;
            Id = id;
            DisplayMember = displayMamber;
            OpenDetailViewCommand = new DelegateCommand(OnOpenDetailViewExecute);
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

        public ICommand OpenDetailViewCommand { get; }

        private void OnOpenDetailViewExecute()
        {
            _eventAggregator.GetEvent<OpenDetailViewEvent>()
                .Publish(
                new OpenDetailViewEventArgs
                {
                    Id = Id,
                    ViewModelName = _detailViewModelName
                });
        }
    }
}
