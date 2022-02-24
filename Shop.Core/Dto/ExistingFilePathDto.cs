﻿using System;


namespace Shop.Core.Dto
{
    public class ExistingFilePathDto
    {
        public Guid Id { get; set; }
        public string ExistingFilePath { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? CarId { get; set; }
        public Guid? SpaceShipId { get; set; }
    }
}
