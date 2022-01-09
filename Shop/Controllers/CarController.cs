﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Core.Dtos;
using Shop.Core.ServiceInterface;
using Shop.Data;
using Shop.Models.Car;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class CarController : Controller
    {
        private readonly ShopDbContext _context;
        private readonly ICarService _carService;
        private readonly IFileServices _fileServices;

        public CarController
            (
            ShopDbContext context,
            ICarService carService,
            IFileServices fileServices
            )
        {
            _context = context;
            _carService = carService;
            _fileServices = fileServices;
        }

        //ListItem
        [HttpGet]
        public IActionResult Index()
        {
            var result = _context.Car
                .OrderByDescending(y => y.CreatedAt)
                .Select(x => new CarListItem
                {
                    Id = x.Id,
                    Brand = x.Brand,
                    ModelName = x.ModelName,
                    Price = x.Price,
                    Color = x.Color,
                    Transmission = x.Transmission,
                    Power = x.Power,
                    Year = x.Year
                });

            return View(result);
        }

        [HttpGet]
        public IActionResult Add()
        {
            CarViewModel model = new CarViewModel();

            return View("Edit", model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CarViewModel model)
        {
            var dto = new CarDto()
            {
                Id = model.Id,
                Brand = model.Brand,
                ModelName = model.ModelName,
                Price = model.Price,
                Color = model.Color,
                Transmission = model.Transmission,
                Power = model.Power,
                Year = model.Year,
                CreatedAt = model.CreatedAt,
                ModifiedAt = model.ModifiedAt,
                Files = model.Files,
                ExistingFilePaths = model.ExistingFilePaths.Select(x => new ExistingFilePathDto
                {
                    Id = x.PhotoId,
                    ExistingFilePath = x.FilePath,
                    CarId = x.CarId
                }).ToArray()
            };

            var result = await _carService.Add(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var car = await _carService.Delete(id);
            if (car == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var car = await _carService.GetAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            var photos = await _context.ExistingFilePath
                .Where(x => x.CarId == id)
                .Select(y => new ExistingFilePathViewModel
                {
                    FilePath = y.FilePath,
                    PhotoId = y.Id
                })
                .ToArrayAsync();

            var model = new CarViewModel();

            model.Id = car.Id;
            model.Brand = car.Brand;
            model.ModelName = car.ModelName;
            model.Price = car.Price;
            model.Color = car.Color;
            model.Transmission = car.Transmission;
            model.Power = car.Power;
            model.Year = car.Year;
            model.ModifiedAt = car.ModifiedAt;
            model.CreatedAt = car.CreatedAt;
            model.ExistingFilePaths.AddRange(photos);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CarViewModel model)
        {
            var dto = new CarDto()
            {
                Id = model.Id,
                Brand = model.Brand,
                ModelName = model.ModelName,
                Price = model.Price,
                Color = model.Color,
                Transmission = model.Transmission,
                Power = model.Power,
                Year = model.Year,
                CreatedAt = model.CreatedAt,
                ModifiedAt = model.ModifiedAt,
                Files = model.Files,
                ExistingFilePaths = model.ExistingFilePaths
                    .Select(x => new ExistingFilePathDto
                    {
                        Id = x.PhotoId,
                        ExistingFilePath = x.FilePath,
                        CarId = x.CarId
                    }).ToArray()
            };

            var result = await _carService.Update(dto);

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