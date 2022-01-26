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

        public SpaceShipServices
            (
            ShopDbContext context
            )
        {
            _context = context;
        }

        public async Task<SpaceShip> Add(SpaceShipDto dto)
        {
            SpaceShip spaceShip = new SpaceShip();
            FileToDatabase file = new FileToDatabase();

            spaceShip.Id = Guid.NewGuid();
            spaceShip.Name = dto.Name;
            spaceShip.ModelName = dto.ModelName;
            spaceShip.Company = dto.Company;
            spaceShip.EnginePower = dto.EnginePower;
            spaceShip.Country = dto.Country;
            spaceShip.LaunchDate = dto.LaunchDate;
            spaceShip.CreatedAt = DateTime.Now;
            spaceShip.ModifiedAt = DateTime.Now;

            if (dto.Files != null)
            {
                file.ImageData = UploadFile(dto, spaceShip);
            }

            await _context.SpaceShip.AddAsync(spaceShip);
            await _context.SaveChangesAsync();

            return spaceShip;
        }


        public async Task<SpaceShip> Delete(Guid id)
        {
            var spaceShipId = await _context.SpaceShip
                .FirstOrDefaultAsync(x => x.Id == id);

            _context.SpaceShip.Remove(spaceShipId);
            await _context.SaveChangesAsync();

            return spaceShipId;
        }


        public async Task<SpaceShip> Update(SpaceShipDto dto)
        {
            SpaceShip spaceShip = new SpaceShip();
            FileToDatabase file = new FileToDatabase();

            spaceShip.Id = dto.Id;
            spaceShip.Name = dto.Name;
            spaceShip.ModelName = dto.ModelName;
            spaceShip.Company = dto.Company;
            spaceShip.EnginePower = dto.EnginePower;
            spaceShip.Country = dto.Country;
            spaceShip.LaunchDate = dto.LaunchDate;
            spaceShip.CreatedAt = dto.CreatedAt;
            spaceShip.ModifiedAt = DateTime.Now;

            if (dto.Files != null)
            {
                file.ImageData = UploadFile(dto, spaceShip);
            }


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

        public byte[] UploadFile(SpaceShipDto dto, SpaceShip spaceship)
        {

            if (dto.Files != null && dto.Files.Count > 0) 
            {
                foreach (var photo in dto.Files)
                {
                    using (var target = new MemoryStream())
                    {
                        FileToDatabase objFiles = new FileToDatabase
                        {
                            Id = Guid.NewGuid(),
                            ImageTitle = photo.FileName,
                            SpaceshipId = spaceship.Id,
                        };

                        photo.CopyTo(target);
                        objFiles.ImageData = target.ToArray();

                        _context.FileToDatabase.Add(objFiles);
                    }
                }
            }

            return null;

        }

        public async Task<FileToDatabase> RemoveImage(FileToDatabaseDto dto)
        {
            var image = await _context.FileToDatabase
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            _context.FileToDatabase.Remove(image);
            await _context.SaveChangesAsync();

            return image;
        }

    }
}
