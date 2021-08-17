﻿using System;
using System.Threading.Tasks;
using Vehicles.API.Data;
using Vehicles.API.Data.Entities;
using Vehicles.API.Models;

namespace Vehicles.API.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IUserHelper _userHelper;

        public ConverterHelper(DataContext context, ICombosHelper combosHelper, IUserHelper userHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
            _userHelper = userHelper;
        }

        public async Task<User> ToUserAsync(UserViewModel model, Guid imageId, bool isNew)
        {
            return new User
            {
                Id = isNew ? Guid.NewGuid().ToString() : model.Id,
                ImageId = imageId,
                Address = model.Address,
                Document = model.Document,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                UserName = model.UserName,
                DocumentType = await _context.DocumentTypes.FindAsync(model.DocumentTypeId),
                UserType = model.UserType,
                Vehicles = model.Vehicles
            };
        }

        public UserViewModel ToUserViewModel(User user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                ImageId = user.ImageId,
                Address = user.Address,
                Document = user.Document,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
                DocumentType = user.DocumentType,
                UserType = user.UserType,
                Vehicles = user.Vehicles,
                DocumentTypeId = user.DocumentType.Id,
                DocumentTypes = _combosHelper.GetComboDocumentTypes()
            };
        }

        public async Task<Vehicle> ToVehicleAsync(VehicleViewModel model, bool isNew)
        {
            return new Vehicle
            {
                Id = isNew ? 0 : model.Id,
                Histories = model.Histories,
                Model = model.Model,
                Plaque = model.Plaque.ToUpper(),
                Line = model.Line,
                Color = model.Color,
                User = model.User,
                VehiclePhotos = model.VehiclePhotos,
                Brand = await _context.Brands.FindAsync(model.BrandId),
                VehicleType = await _context.VehicleTypes.FindAsync(model.VehicleTypeId),
            };
        }

        public VehicleViewModel ToVehicleViewModel(Vehicle vehicle)
        {
            return new VehicleViewModel
            {
                Brand = vehicle.Brand,
                BrandId = vehicle.Brand.Id,
                Brands = _combosHelper.GetComboBrands(),
                Color = vehicle.Color,
                Histories = vehicle.Histories,
                Id = vehicle.Id,
                Line = vehicle.Line,
                Model = vehicle.Model,
                Plaque = vehicle.Plaque.ToUpper(),
                User = vehicle.User,
                VehiclePhotos = vehicle.VehiclePhotos,
                VehicleType = vehicle.VehicleType,
                VehicleTypeId = vehicle.VehicleType.Id,
                VehicleTypes = _combosHelper.GetComboVehicleTypes()
            };
        }
    }
}
