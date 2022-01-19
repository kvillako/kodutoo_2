using Shop.Core.Domain;
using Shop.Core.Dtos;
using Shop.Data;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Core.ServiceInterface;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Linq;

namespace Shop.ApplicationServices.Services
{
    public class SpaceShipServices : ISpaceShipService
    {
        private readonly ShopDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IFileServices _fileServices;

        public SpaceShipServices
            (
            ShopDbContext context,
            IWebHostEnvironment env,
            IFileServices fileServices
            )
        {
            _context = context;
            _env = env;
            _fileServices = fileServices;
        }

        public async Task<SpaceShip> Add(SpaceShipDto dto)
        {
            SpaceShip spaceShip = new SpaceShip();

            spaceShip.Id = Guid.NewGuid();
            spaceShip.Name = dto.Name;
            spaceShip.ModelName = dto.ModelName;
            spaceShip.Company = dto.Company;
            spaceShip.EnginePower = dto.EnginePower;
            spaceShip.Country = dto.Country;
            spaceShip.LaunchDate = dto.LaunchDate;

            spaceShip.CreatedAt = DateTime.Now;
            spaceShip.ModifiedAt = DateTime.Now;
            _fileServices.ProcessUploadFile(dto, spaceShip);

            await _context.SpaceShip.AddAsync(spaceShip);
            await _context.SaveChangesAsync();

            return spaceShip;
        }


        public async Task<SpaceShip> Delete(Guid id)
        {
            var spaceShipId = await _context.SpaceShip
                .Include(x => x.ExistingFilePaths)
                .FirstOrDefaultAsync(x => x.Id == id);

            var photos = await _context.ExistingFilePath
                .Where(x => x.SpaceShipId == id)
                .Select(y => new ExistingFilePathDto
                {
                    SpaceShipId = y.SpaceShipId,
                    ExistingFilePath = y.FilePath,
                    Id = y.Id
                })
                .ToArrayAsync();


            await _fileServices.RemoveImages(photos);
            _context.SpaceShip.Remove(spaceShipId);
            await _context.SaveChangesAsync();

            return spaceShipId;
        }


        public async Task<SpaceShip> Update(SpaceShipDto dto)
        {
            SpaceShip spaceShip = new SpaceShip();

            spaceShip.Id = dto.Id;
            spaceShip.Name = dto.Name;
            spaceShip.ModelName = dto.ModelName;
            spaceShip.Company = dto.Company;
            spaceShip.EnginePower = dto.EnginePower;
            spaceShip.Country = dto.Country;
            spaceShip.LaunchDate = dto.LaunchDate;

            spaceShip.CreatedAt = dto.CreatedAt;
            spaceShip.ModifiedAt = DateTime.Now;
            _fileServices.ProcessUploadFile(dto, spaceShip);

            _context.SpaceShip.Update(spaceShip);
            await _context.SaveChangesAsync();
            return spaceShip;
        }

        public async Task<SpaceShip> GetAsync(Guid id)
        {
            var result = await _context.SpaceShip
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

    }
}
