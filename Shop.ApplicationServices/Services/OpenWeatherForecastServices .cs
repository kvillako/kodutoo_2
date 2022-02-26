﻿using System.Net;
using System;
using Nancy.Json;
using Shop.Core.ServiceInterface;
using Shop.Core.Dto.OpenWeather;
using System.Threading.Tasks;

namespace Shop.ApplicationServices.Services
{
    public class OpenWeatherForecastServices : IOpenWeatherForecastServices
    {

        public async Task<OpenWeatherResultDto> OpenWeatherDetail(OpenWeatherResultDto dto)
        {
            string apikey = "0e10592fc0050c2550afc36c4fbc8348";
            var cityName = dto.City.ToString();
            var url = "https://api.openweathermap.org/data/2.5/weather?units=metric" + "&q=" + cityName + "&appid=" + apikey;

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);
                OpenWeatherRootDto weatherInfo = (new JavaScriptSerializer()).Deserialize<OpenWeatherRootDto>(json);

                dto.Temperature = weatherInfo.Main.temp;
                dto.TempFeelsLike = weatherInfo.Main.feels_like;
                dto.Humidity = weatherInfo.Main.humidity;
                dto.Pressure = weatherInfo.Main.pressure;
                dto.WindSpeed = weatherInfo.Wind.speed;
                dto.WeatherCondition = weatherInfo.Weather[0].description;

                var jsonString = new JavaScriptSerializer().Serialize(dto);
            }
            
            return dto;
        }
    }
}
