﻿using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using System.Windows.Input;
using FriendOrganizerN2.UI.Event;
using FriendOrganizerN2.UI.View.Services;

namespace FriendOrganizerN2.UI.ViewModel
{
    public abstract class DetailViewModelBase : ViewModelBase, IDetailViewModel
    {
        private int _id;
        private bool _hasChanges;
        protected readonly IEventAggregator EventAggregator;
        protected readonly IMessageDialogService MessageDialogService;

        private string _title;

        public DetailViewModelBase(IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            EventAggregator = eventAggregator;
            MessageDialogService = messageDialogService;
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);
            CloseDetailViewModelCommand = new DelegateCommand(OnCloseDetailViewModelExecute);
        }

        public abstract Task LoadAsync(int id);

        public ICommand SaveCommand { get; }

        public ICommand DeleteCommand { get; }

        public ICommand CloseDetailViewModelCommand { get; }

        public int Id
        {
            get { return _id; }
            protected set { _id = value; }
        }



        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }


        public bool HasChanges
        {
            get { return _hasChanges; }
            set
            {
                _hasChanges = value;
                OnPropertyChanged();
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        protected abstract void OnDeleteExecute();
        protected abstract bool OnSaveCanExecute();
        protected abstract void OnSaveExecute();



        protected virtual void RaiseDetailDeletedEvent(int modelId)
        {
            EventAggregator.GetEvent<AfterDetailDeletedEvent>().Publish(
                new AfterDetailDeletedEventArgs
                {
                    Id = modelId,
                    ViewModelName = this.GetType().Name
                });
        }

        protected virtual void RaiseDetailSavedEvent(int modelId, string displayMember)
        {
            EventAggregator.GetEvent<AfterDetailSavedEvent>().Publish(new AfterDetailSavedEventArgs
            {
                Id = modelId,
                DisplayMember = displayMember,
                ViewModelName = this.GetType().Name
            });
        }

        protected virtual void RaiseCollectionSavedEvent()
        {
            EventAggregator.GetEvent<AfterCollectionSavedEvent>().Publish(new AfterCollectionSavedEventArgs
            {
                ViewModelName = this.GetType().Name
            });
        }
        protected virtual void OnCloseDetailViewModelExecute()
        {
            if (HasChanges)
            {
                var result = MessageDialogService.ShowOkCancelDialog("You've made changes. Close this tab ?", "Question");

                if (result == MessageDialogResult.Cancel)
                {
                    return;
                }

            }

            EventAggregator.GetEvent<AfterDetailClosedEvent>().Publish(
                new AfterDetailClosedEventAegs
                {
                    Id = this.Id,
                    ViewModelName = this.GetType().Name
                });
        }

    }
}
