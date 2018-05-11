using System;
using System.Threading.Tasks;
using FriendOrganizer.Model;
using FriendOrganizerN2.UI.Data.Repositories;
using FriendOrganizerN2.UI.View.Services;
using FriendOrganizerN2.UI.Wrapper;
using Prism.Commands;
using Prism.Events;

namespace FriendOrganizerN2.UI.ViewModel
{
    public class MeetingDetailViewModel : DetailViewModelBase, IMeetingDetailViewModel
    {
        private IMessageDialogService _messageDialogService;
        private MeetingWrapper _meeting;
        private IMeetingRepository _meetingRepository;

        public MeetingDetailViewModel(IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService,
            IMeetingRepository meetingRepository) : base(eventAggregator)
        {
            _messageDialogService = messageDialogService;
            _meetingRepository = meetingRepository;

        }


        public MeetingWrapper Meeting
        {
            get { return _meeting; }
            private set
            {
                _meeting = value;
                OnPropertyChanged();
            }
        }

        public async override Task LoadAsync(int? meetingId)
        {
            var meeting = meetingId.HasValue
                  ? await _meetingRepository.GetByIdAsync(meetingId.Value)
                  : CreateNewMeeting();

            InitializeMeeting(meeting);
        }

        protected override void OnDeleteExecute()
        {
            var result = _messageDialogService
                .ShowOkCancelDialog($"Do you Realy want to delete this meeting {Meeting.Title}?", "Question");
            if (result == MessageDialogResult.OK)
            {
                _meetingRepository.Remove(Meeting.Model);
                _meetingRepository.SaveAsync();
                RaiseDetailDeletedEvent(Meeting.Id);
            }
        }

        protected override bool OnSaveCanExecute()
        {
            return Meeting != null && !Meeting.HasErrors && HasChanges;
        }

        protected async override void OnSaveExecute()
        {
            await _meetingRepository.SaveAsync();
            HasChanges = _meetingRepository.HasChanges();
            RaiseDetailSavedEvent(Meeting.Id, Meeting.Title);
        }


        private void InitializeMeeting(Meeting meeting)
        {
            Meeting = new MeetingWrapper(meeting);
            Meeting.PropertyChanged += (s, e) =>
            { 
                if (!HasChanges)
                {
                    HasChanges = _meetingRepository.HasChanges();
                }

                if (e.PropertyName == nameof(Meeting.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

            if (Meeting.Id == 0)
            {
                // Little trick to trigger validation
                Meeting.Title = "";
            }
        }

        private Meeting CreateNewMeeting()
        {
            var meeting = new Meeting
            {
                DateFrom = DateTime.Now,
                DateTo = DateTime.Now
            };

            _meetingRepository.Add(meeting);
            return meeting;
        }

    }
}
