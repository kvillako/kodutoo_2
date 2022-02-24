using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Core.Dto.Weather
{
    public class TemperatureDto
    {
        public MinimumDto Minimum { get; set; }
        public MaximumDto Maximum { get; set; }
    }
}
