using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Shop.Core.Dto.OpenWeather
{
    public class maindto
    {
        [JsonProperty("temp")]
        public double temp { get; set; }

        [JsonProperty("feels_like")]
        public double feels_like { get; set; }

        [JsonProperty("humidity")]
        public double humidity { get; set; }

        [JsonProperty("pressure")]
        public double pressure { get; set; }

    }
}
