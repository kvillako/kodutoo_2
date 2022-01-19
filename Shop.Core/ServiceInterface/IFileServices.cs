using Shop.Core.Domain;
using Shop.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.ServiceInterface
{
    public interface IFileServices : IApplicationService
    {
        string ProcessUploadFile(ProductDto dto, Product product);

        string ProcessUploadFile(CarDto dto, Car car);
        string ProcessUploadFile(SpaceShipDto dto, SpaceShip spaceShip);

        Task<ExistingFilePath> RemoveImage(ExistingFilePathDto dto);
        Task<List<ExistingFilePath>> RemoveImages(ExistingFilePathDto[] dto);
    }
}
