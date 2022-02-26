using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Shop.Core.Dto.OpenWeather
{
    public class winddto
    {
        [JsonProperty("speed")]
        public double speed { get; set; }
    }
}
