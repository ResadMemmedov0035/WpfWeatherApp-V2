using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MainApp.Messages;
using MainApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Microsoft.Maps.MapControl.WPF;

namespace MainApp.ViewModels
{
    class DetailsPageVM : ViewModelBase
    {
        private IMessenger Messenger;


        private CityWeather city;
        private Location currLocation;

        public CityWeather City { get => city; set => Set(ref city, value); }

        public Location CurrLocation { get => currLocation; set => Set(ref currLocation, value); }


        private RelayCommand backCommand;

        public RelayCommand BackCommand => backCommand ??= new RelayCommand(() =>
        {
            Messenger.Send(new NavigationMessage() { ViewModel = App.Container.GetInstance<ListPageVM>() });
        });


        public DetailsPageVM(IMessenger messenger)
        {
            Messenger = messenger;

            Messenger.Register<CityWeatherMessage>(this, message =>
            {
                City = message.City;
                CurrLocation = new Location(City.coord.lat, City.coord.lon);
            });

        }
    }
}
