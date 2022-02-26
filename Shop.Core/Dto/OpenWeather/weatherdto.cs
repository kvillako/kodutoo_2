using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Shop.Core.Dto.OpenWeather
{
    public class weatherdto
    {
        [JsonProperty("weather.description")]
        public string description { get; set; }
    }
}
