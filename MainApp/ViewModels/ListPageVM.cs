using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MainApp.Commands;
using MainApp.Messages;
using MainApp.Models;
using MainApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace MainApp.ViewModels
{
    class ListPageVM : ViewModelBase
    {
        private IMessenger Messenger;


        private RelayCommand addCityCommand;
        private RelayCommand openCityCommand;

        public RelayCommand AddCityCommand => addCityCommand ??= new RelayCommand(() =>
        {
            Messenger.Send(new NavigationMessage() { ViewModel = App.Container.GetInstance<AddPageVM>() });
        });

        public RelayCommand OpenCityCommand => openCityCommand ??= new RelayCommand(() =>
        {
            Messenger.Send(new NavigationMessage() { ViewModel = App.Container.GetInstance<DetailsPageVM>() });
            Messenger.Send(new CityWeatherMessage() { City = SelectedCity });
        },
        () => SelectedCity != null );


        private ObservableCollection<CityWeather> cities;
        private CityWeather selectedCity;

        public ObservableCollection<CityWeather> Cities { get => cities; set => Set(ref cities, value); }

        public CityWeather SelectedCity 
        {
            get => selectedCity;
            set
            {
                Set(ref selectedCity, value);
                OpenCityCommand.RaiseCanExecuteChanged();
            }
        }


        public ListPageVM(IMessenger messenger)
        {
            Messenger = messenger;
            Cities = new ObservableCollection<CityWeather>();

            Messenger.Register<CityWeatherMessage>(this, message =>
            {
                if(!Cities.Contains(message.City))
                Cities.Add(message.City);
            });
        }

    }
}
