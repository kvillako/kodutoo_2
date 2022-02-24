using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Core.Dto.Weather
{
    public class DegreeDaySummaryDto
    {
        public HeatingDto Heating { get; set; }
        public CoolingDto Cooling { get; set; }
    }
}
