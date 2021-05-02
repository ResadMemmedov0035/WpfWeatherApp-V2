using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MainApp.Messages;
using MainApp.Models;
using MainApp.Validators;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Windows;

namespace MainApp.ViewModels
{
    class AddPageVM : ViewModelBase, IDataErrorInfo
    {
        private IMessenger Messenger;
        private Services.WeatherService WService;


        private RelayCommand addCommand;
        private RelayCommand cancelCommand;

        public RelayCommand AddCommand => addCommand ??= new RelayCommand(() =>
        {
            Messenger.Send(new CityWeatherMessage() { City = SendCityWeather() });
            Messenger.Send(new NavigationMessage() { ViewModel = App.Container.GetInstance<ListPageVM>() });
        },
        () =>
        {
            return this["CityName"] == string.Empty ||
                   this["Latitude"] == string.Empty && this["Longitude"] == string.Empty;
        });

        public RelayCommand CancelCommand => cancelCommand ??= new RelayCommand(() =>
        {
            Messenger.Send(new NavigationMessage() { ViewModel = App.Container.GetInstance<ListPageVM>() });
        });


        private string cityName;
        private string latitude;
        private string longitude;
        private bool isCheckedByName = true;
        private bool isCheckedByCoords;
        private Location currLoc = new Location();

        //[Required]
        public string CityName
        {
            get => cityName;
            set
            {
                Set(ref cityName, value);
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        //[Required]
        //[Latitude(ErrorMessage = "Latitude value is invalid")]
        public string Latitude
        {
            get => latitude;
            set
            {
                Set(ref latitude, value);
                AddCommand.RaiseCanExecuteChanged();
                if(this["Latitude"] == string.Empty) CurrLoc.Latitude = double.Parse(latitude);
            }
        }

        //[Required]
        //[Longitude(ErrorMessage = "Longitude value is invalid")]
        public string Longitude
        {
            get => longitude;
            set
            {
                Set(ref longitude, value);
                AddCommand.RaiseCanExecuteChanged();
                if(this["Longitude"] == string.Empty) CurrLoc.Longitude = double.Parse(Longitude);
            }
        }

        public bool IsCheckedByName { get => isCheckedByName; set => Set(ref isCheckedByName, value); }

        public bool IsCheckedByCoords { get => isCheckedByCoords; set => Set(ref isCheckedByCoords, value); }

        public Location CurrLoc{ get => currLoc; set => Set(ref currLoc, value); }

        public string Error { get; }

        public string this[string columnName] {
            get 
            {
                var validator = App.Container.GetInstance<AddPageVmValidator>();
                var result = validator.Validate(this);

                if (result.IsValid)
                    return string.Empty;

                var failure = result.Errors.FirstOrDefault(x => x.PropertyName == columnName);

                if (failure is null)
                    return string.Empty;

                return failure.ErrorMessage;

                //var context = new ValidationContext(this);
                //var results = new List<ValidationResult>();
                //var isValid = Validator.TryValidateObject(this, context, results, true);

                //if (isValid)
                //    return string.Empty;

                //var result = results.FirstOrDefault(x => x.MemberNames.Contains(columnName));

                //if (result is null)
                //    return string.Empty;

                //return result.ErrorMessage;
            }
        }


        public AddPageVM(IMessenger messenger)
        {
            Messenger = messenger;
            WService = new Services.WeatherService();
            messenger.Register<LocationMessage>(this, message =>
            {
                Latitude = message.Latitude.ToString();
                Longitude = message.Longitude.ToString();
            });
        }


        private CityWeather SendCityWeather()
        {
            if (IsCheckedByName)
            {
                return WService.GetWeatherByName(CityName);
            }
            else if (IsCheckedByCoords)
            {
                var lat = float.Parse(Latitude);
                var lon = float.Parse(Longitude);
                return WService.GetWeatherByCoords(lat, lon);
            }
            return null;
        }
    }

    //class LatitudeAttribute : ValidationAttribute
    //{
    //    public override bool IsValid(object value)
    //    {
    //        if (value is string latitude) 
    //        {
    //            if (float.TryParse(latitude, out float lat)) 
    //            {
    //                return -90 <= lat && lat <= 90;
    //            }
    //        }
    //        return false;
    //    }
    //}

    //class LongitudeAttribute : ValidationAttribute
    //{
    //    public override bool IsValid(object value)
    //    {
    //        if (value is string longitude)
    //        {
    //            if (float.TryParse(longitude, out float lon))
    //            {
    //                return -180 <= lon && lon <= 180;
    //            }
    //        }
    //        return false;
    //    }
    //}
}
