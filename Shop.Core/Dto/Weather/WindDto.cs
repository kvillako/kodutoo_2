using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Core.Dto.Weather
{
    public class WindDto
    {
        public SpeedDto Speed { get; set; }
        public DirectionDto Direction { get; set; }
    }
}
