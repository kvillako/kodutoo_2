using System.Collections.Generic;

namespace Shop.Core.Dto.OpenWeather
{
    public class OpenWeatherRootDto
    {
        public maindto Main { get; set; }

        public winddto Wind { get; set; }

        public List<weatherdto> Weather { get; set; }

    }
}