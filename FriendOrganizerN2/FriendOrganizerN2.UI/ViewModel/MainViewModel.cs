﻿using FriendOrganizerN2.UI.Event;
using FriendOrganizerN2.UI.View.Services;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Autofac.Features.Indexed;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using FriendOrganizerN2.UI.Extentions;

namespace FriendOrganizerN2.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private IDetailViewModel _selectedDetailViewModel;
        private IEventAggregator _eventAggregator;
        private IIndex<string, IDetailViewModel> _detailViewModelCreator;
        private bool _isLoading;

        private IMessageDialogService _messageDialogService;

        public MainViewModel(INavigationViewModel navigationViewModel,
            IIndex<string, IDetailViewModel> detailViewModelCreator,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            _eventAggregator = eventAggregator;
            _detailViewModelCreator = detailViewModelCreator;
            _messageDialogService = messageDialogService;

            DetailViewModels = new ObservableCollection<IDetailViewModel>();


            _eventAggregator.GetEvent<OpenDetailViewEvent>().Subscribe(OnOpenDetailView);
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>().Subscribe(AfterDetailDeleted);
            _eventAggregator.GetEvent<AfterDetailClosedEvent>().Subscribe(AfterDetailClosed);

            CreateNewDetailCommand = new DelegateCommand<Type>(OnCreateNewDetailExecute);
            OpenSingleDetailViewCommand = new DelegateCommand<Type>(OnOpenSingleDetailViewExecute);
            DetailMouserOverCommand = new DelegateCommand<Type>(OnDetailMouserOverExecute);
            hamburgClickCommand = new DelegateCommand(OnhamburgClickExecute);
            PinClickCommand = new DelegateCommand(OnPinClickedExecute);

            NavigationViewModel = navigationViewModel;

        }

        private bool isPinned = false;
        private void OnPinClickedExecute()
        {
            if (isPinned)
            {
                isPinned = false;
                OnhamburgClickExecute();
                //MainWindowView.pinToggleBtn.Content = "&#xE840;";
                //MainWindowView.pinToggleBtn.FontFamily = 
                if (!MainWindowView.mainAreaGrid.ColumnDefinitions.Contains(MainWindowView.columnAddOrRemove))
                {
                    MainWindowView.mainAreaGrid.ColumnDefinitions.Insert(0, MainWindowView.columnAddOrRemove);
                }
            }
            else
            {
                isPinned = true;
                MainWindowView.pinToggleBtn.Content = "&#xE718;";
                if (MainWindowView.mainAreaGrid.ColumnDefinitions.Contains(MainWindowView.columnAddOrRemove))
                {
                    MainWindowView.mainAreaGrid.ColumnDefinitions.Remove(MainWindowView.columnAddOrRemove);
                }
            }

        }

        public async Task LoadAsync()
        {
            IsLoading = true;
            await NavigationViewModel.LoadAsync();
            IsLoading = false;
        }

        public MainWindow MainWindowView;
        public ICommand CreateNewDetailCommand { get; }
        public ICommand OpenSingleDetailViewCommand { get; }
        public ICommand DetailMouserOverCommand { get; }
        public ICommand hamburgClickCommand { get; }
        public ICommand PinClickCommand { get; }
        public INavigationViewModel NavigationViewModel { get; }



        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<IDetailViewModel> DetailViewModels { get; }

        public IDetailViewModel SelectedDetailViewModel
        {
            get { return _selectedDetailViewModel; }
            set
            {
                _selectedDetailViewModel = value;
                OnPropertyChanged();
            }
        }


        private async void OnOpenDetailView(OpenDetailViewEventArgs args)
        {

            var detailViewModel = DetailViewModels.SingleOrDefault(vm => vm.Id == args.Id
                    && vm.GetType().Name == args.ViewModelName);

            if (detailViewModel == null)
            {
                detailViewModel = _detailViewModelCreator[args.ViewModelName];
                try
                {

                    await detailViewModel.LoadAsync(args.Id);
                }
                catch
                {
                    await _messageDialogService.ShowInfoAsync("Could not load entity, " +
                        "maybe it was deleted in the meantime by another user, " +
                        "Navigation will be refreshed for you.");
                    await NavigationViewModel.LoadAsync();
                    return;
                }
                DetailViewModels.Add(detailViewModel);
            }

            SelectedDetailViewModel = detailViewModel;

        }

        private int nextNewItemId = 0;
        private void OnCreateNewDetailExecute(Type viewModelType)
        {
            OnOpenDetailView(
                new OpenDetailViewEventArgs
                {
                    Id = nextNewItemId,
                    ViewModelName = viewModelType.Name
                });
        }

        private void OnOpenSingleDetailViewExecute(Type viewModelType)
        {
            OnOpenDetailView(
                new OpenDetailViewEventArgs
                {
                    Id = -1,
                    ViewModelName = viewModelType.Name
                });
        }

        private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            RemoveDetailViewModel(args.Id, args.ViewModelName);
        }

        private void AfterDetailClosed(AfterDetailClosedEventAegs args)
        {
            RemoveDetailViewModel(args.Id, args.ViewModelName);
        }

        private void RemoveDetailViewModel(int id, string viewModelName)
        {
            var detailViewModel = DetailViewModels.SingleOrDefault(vm => vm.Id == id
                                            && vm.GetType().Name == viewModelName);

            if (detailViewModel != null)
            {
                DetailViewModels.Remove(detailViewModel);
            }
        }



        private void OnhamburgClickExecute()
        {

            MainWindowView.navigationTransform.AnimateTo(new Point());
        }

        private void OnDetailMouserOverExecute(Type obj)
        {
            if (isPinned)
            {
                GeneralTransform generalTransform = MainWindowView.TransformToVisual(MainWindowView.navigationGrid);
                Point point = generalTransform.Transform(new Point());

                point.X += MainWindowView.navigationTransform.X - MainWindowView.navigationColumn.ActualWidth;
                point.Y = 0; // for transforming only X axis
                MainWindowView.navigationTransform.AnimateTo(point);

            }

        }

    }
}
