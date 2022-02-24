using System;
using System.Collections.Generic;
using System.Text;
using Shop.Core.Dto.Weather;
using System.Threading.Tasks;

namespace Shop.Core.ServiceInterface
{
    public interface IWeatherForecastServices : IApplicationService
    {
        Task<WeatherResultDto> WeatherDetail(WeatherResultDto dto);
    }
}
