﻿using Shop.Core.Domain;
using Shop.Core.Dto;
using Shop.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.ServiceInterface
{
    public interface ISpaceShipService : IApplicationService //mul seda viimast polnud.
    {
        Task<SpaceShip> Add(SpaceShipDto dto);

        Task<SpaceShip> Delete(Guid id);

        Task<SpaceShip> Update(SpaceShipDto dto);

        Task<SpaceShip> GetAsync(Guid id);

        Task<FileToDatabase> RemoveImage(FileToDatabaseDto dto);

        //Task<ExistingFilePath> RemoveImage(ExistingFilePathDto dto);
    }
}
