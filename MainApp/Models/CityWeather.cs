using System;
using System.Collections.Generic;
using System.Text;

namespace MainApp.Models
{

    public class CityWeather
    {
        public Coord coord { get; set; }
        public Weather[] weather { get; set; }
        public string _base { get; set; }
        public Main main { get; set; }
        public int visibility { get; set; }
        public Wind wind { get; set; }
        public Rain rain { get; set; }
        public Clouds clouds { get; set; }
        public int dt { get; set; }
        public Sys sys { get; set; }
        public int timezone { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }

        public bool IsCold => main.temp < 15;

        public string IconUrl => $"http://openweathermap.org/img/wn/{weather[0].icon}@2x.png";
    }


}
