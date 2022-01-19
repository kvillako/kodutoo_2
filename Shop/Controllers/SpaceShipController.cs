using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IFileServices _fileServices;

        public SpaceShipController
            (
            ShopDbContext context,
            ISpaceShipService spaceShipService,
            IFileServices fileServices
            )
        {
            _context = context;
            _spaceShipService = spaceShipService;
            _fileServices = fileServices;
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
                ExistingFilePaths = model.ExistingFilePaths.Select(x => new ExistingFilePathDto
                {
                    Id = x.PhotoId,
                    ExistingFilePath = x.FilePath,
                    SpaceShipId = x.SpaceShipId
                }).ToArray()
            };

            var result = await _spaceShipService.Add(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var car = await _spaceShipService.Delete(id);
            if (car == null)
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

            var photos = await _context.ExistingFilePath
                .Where(x => x.SpaceShipId == id)
                .Select(y => new ExistingFilePathViewModel
                {
                    FilePath = y.FilePath,
                    PhotoId = y.Id
                })
                .ToArrayAsync();

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
            model.ExistingFilePaths.AddRange(photos);

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
                ExistingFilePaths = model.ExistingFilePaths
                    .Select(x => new ExistingFilePathDto
                    {
                        Id = x.PhotoId,
                        ExistingFilePath = x.FilePath,
                        SpaceShipId = x.SpaceShipId
                    }).ToArray()
            };

            var result = await _spaceShipService.Update(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), model);
        }


        [HttpPost]
        public async Task<IActionResult> RemoveImage(ExistingFilePathViewModel model)
        {
            var dto = new ExistingFilePathDto()
            {
                Id = model.PhotoId
            };

            var photo = await _fileServices.RemoveImage(dto);
            if (photo == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
