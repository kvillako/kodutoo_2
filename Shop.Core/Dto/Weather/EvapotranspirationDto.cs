using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Shop.Core.Dto.Weather
{
    public class EvapotranspirationDto
    {
        [JsonProperty("DailyForecasts.Day.Evapotranspiration.Value")]
        public double Value { get; set; }

        [JsonProperty("DailyForecasts.Day.Evapotranspiration.Unit")]
        public string Unit { get; set; }

        [JsonProperty("DailyForecasts.Day.Evapotranspiration.UnitType")]
        public int UnitType { get; set; }
    }
}
