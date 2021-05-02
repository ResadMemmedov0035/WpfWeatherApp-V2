using MainApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MainApp.Messages
{
    class CityWeatherMessage : IMessage
    {
        public CityWeather City { get; set; }
    }
}
