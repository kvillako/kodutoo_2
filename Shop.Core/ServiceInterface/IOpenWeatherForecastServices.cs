using System;
using System.Collections.Generic;
using System.Text;
using Shop.Core.Dto.OpenWeather;
using System.Threading.Tasks;

namespace Shop.Core.ServiceInterface
{
    public interface IOpenWeatherForecastServices : IApplicationService
    {
        Task<OpenWeatherResultDto> OpenWeatherDetail(OpenWeatherResultDto dto);
    }
}
