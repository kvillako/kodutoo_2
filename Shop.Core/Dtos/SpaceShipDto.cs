﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Core.Dtos
{
    public class SpaceShipDto
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

        public List<IFormFile> Files { get; set; }

        public IEnumerable<FileToDatabaseDto> Image { get; set; }
        = new List<FileToDatabaseDto>();

        //public IEnumerable<ExistingFilePathDto> ExistingFilePaths { get; set; }
        //    = new List<ExistingFilePathDto>();
    }
}
