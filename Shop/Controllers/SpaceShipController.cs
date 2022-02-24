using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Core.Dto;
using Shop.Core.Dtos;
using Shop.Core.ServiceInterface;
using Shop.Data;
using Shop.Models.SpaceShip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class SpaceShipController : Controller
    {
        private readonly ShopDbContext _context;
        private readonly ISpaceShipService _spaceShipService;

        public SpaceShipController
            (
            ShopDbContext context,
            ISpaceShipService spaceShipService
            )
        {
            _context = context;
            _spaceShipService = spaceShipService;
        }

        //ListItem
        [HttpGet]
        public IActionResult Index()
        {
            var result = _context.SpaceShip
                .OrderByDescending(y => y.CreatedAt)
                .Select(x => new SpaceShipListItem
                {
                    Id = x.Id,
                    Name = x.Name,
                    ModelName = x.ModelName,
                    Company = x.Company,
                    EnginePower = x.EnginePower,
                    Country = x.Country,
                    LaunchDate = x.LaunchDate,
                    CreatedAt = x.CreatedAt,
                    ModifiedAt = x.ModifiedAt
                });

            return View(result);
        }

        [HttpGet]
        public IActionResult Add()
        {
            SpaceShipViewModel model = new SpaceShipViewModel();

            return View("Edit", model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SpaceShipViewModel model)
        {
            var dto = new SpaceShipDto()
            {
                Id = model.Id,
                Name = model.Name,
                ModelName = model.ModelName,
                Company = model.Company,
                EnginePower = model.EnginePower,
                Country = model.Country,
                LaunchDate = model.LaunchDate,
                CreatedAt = model.CreatedAt,
                ModifiedAt = model.ModifiedAt,
                Files = model.Files,

                Image = model.Image.Select(x => new FileToDatabaseDto 
                {
                    Id = x.ImageId,
                    ImageData = x.ImageData,
                    ImageTitle = x.ImageTitle,
                    SpaceshipId = x.SpaceshipId,

                }).ToArray()
            };

            var result = await _spaceShipService.Add(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var spaceShip = await _spaceShipService.Delete(id);
            if (spaceShip == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var spaceShip = await _spaceShipService.GetAsync(id);

            if (spaceShip == null)
            {
                return NotFound();
            }

            var photos = await _context.FileToDatabase
                .Where(x => x.SpaceshipId == id)
                .Select(y => new ImageViewModel
                {
                    ImageData = y.ImageData,
                    ImageId = y.Id,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData)),
                    ImageTitle = y.ImageTitle,
                    SpaceshipId = y.Id
                }).ToArrayAsync();

            var model = new SpaceShipViewModel();

            model.Id = spaceShip.Id;
            model.Name = spaceShip.Name;
            model.ModelName = spaceShip.ModelName;
            model.Company = spaceShip.Company;
            model.EnginePower = spaceShip.EnginePower;
            model.Country = spaceShip.Country;
            model.LaunchDate = spaceShip.LaunchDate;
            model.ModifiedAt = spaceShip.ModifiedAt;
            model.CreatedAt = spaceShip.CreatedAt;
            model.Image.AddRange(photos);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SpaceShipViewModel model)
        {
            var dto = new SpaceShipDto()
            {
                Id = model.Id,
                Name = model.Name,
                ModelName = model.ModelName,
                Company = model.Company,
                EnginePower = model.EnginePower,
                Country = model.Country,
                LaunchDate = model.LaunchDate,
 
                CreatedAt = model.CreatedAt,
                ModifiedAt = model.ModifiedAt,
                Files = model.Files,
                Image = model.Image.Select(x => new FileToDatabaseDto
                { 
                    Id = x.ImageId,
                    ImageData = x.ImageData,
                    ImageTitle = x.ImageTitle,
                    SpaceshipId = x.SpaceshipId

                })
            };

            var result = await _spaceShipService.Update(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), model);
        }


        [HttpPost]
        public async Task<IActionResult> RemoveImage(ImageViewModel file)
        {
            var dto = new FileToDatabaseDto()
            {
                Id = file.ImageId
            };

            var image = await _spaceShipService.RemoveImage(dto);
            if (image == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
