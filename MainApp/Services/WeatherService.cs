using MainApp.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;

namespace MainApp.Services
{
    class WeatherService : IWeatherService
    {
        private WebClient web;
        private string URL;
        private const string KEY = "766f6a924fc8abf3c9b5ab76d834d477";

        public WeatherService()
        {
            web = new WebClient();
            URL = "http://api.openweathermap.org/data/2.5/weather?units=metric&appid=" + KEY;
        }

        public CityWeather GetWeatherByName(string name)
        {
            var json = web.DownloadString(URL + $"&q={name}");
            return JsonSerializer.Deserialize<CityWeather>(json);
        }

        public CityWeather GetWeatherByCoords(float lat, float lon)
        {
            var json = web.DownloadString(URL + $"&lat={lat}&lon={lon}");
            return JsonSerializer.Deserialize<CityWeather>(json);
        }
    }
}
