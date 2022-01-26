using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models.SpaceShip
{
    public class SpaceShipViewModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string ModelName { get; set; }
        public string Company { get; set; }
        public int EnginePower { get; set; }
        public string Country { get; set; }
        public DateTime LaunchDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public List<ImageViewModel> Image { get; set; } = new List<ImageViewModel>();

        public List<IFormFile> Files { get; set; }
    }
}
