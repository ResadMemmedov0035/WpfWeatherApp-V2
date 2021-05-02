using MainApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MainApp.Services
{
    interface IWeatherService
    {
        public CityWeather GetWeatherByName(string name);

        public CityWeather GetWeatherByCoords(float latitute, float longitute);
    }
}
