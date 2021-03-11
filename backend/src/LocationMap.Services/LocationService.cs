using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using LocationMap.DataLayer.Context;
using LocationMap.DomainClasses;
using LocationMap.DomainClasses.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LocationMap.Services
{
    public interface ILocationService
    {
        Task<ResultShow<IEnumerable<LocationType>>> GetAllLocationType();
        Task<ResultShow<LocationViewModel>> SubmitLocation(LocationViewModel model);
        Task<ResultShow<IEnumerable<LocationViewModel>>> GetAllLocation();
        Task<ResultShow<LocationViewModel>> SelectLocation(int? id);
    }

    public class LocationService : ILocationService
    {
        private readonly IUnitOfWork _uow;
        private readonly DbSet<LocationType> _locationTypes;
        private readonly DbSet<Location> _location;

        public LocationService(IUnitOfWork uow)
        {
            _uow = uow;
            _locationTypes = _uow.Set<LocationType>();
            _location = _uow.Set<Location>();
        }

        public async Task<ResultShow<LocationViewModel>> SelectLocation(int? id)
        {
            if ((id ?? 0) == 0)
            {
                return new ResultShow<LocationViewModel>()
                {
                    Status = false,
                    DataValue = ErrorHandling.nullValue.ToString()
                };
            }

            var find = await _location.FirstOrDefaultAsync(x => x.Id == id);
            if (find == null)
            {
                return new ResultShow<LocationViewModel>()
                {
                    Status = false,
                    DataValue = ErrorHandling.notfound.ToString()
                };
            }

            return new ResultShow<LocationViewModel>()
            {
                Status = true,
                DataValueObject = new LocationViewModel()
                {
                    Id = find.Id,
                    LocationName = find.LocationName,
                    LocationTypeId = find.LocationTypeId,
                    Lat = find.Lat,
                    Lng = find.Lng,
                    FileName = find.FileName,
                    LocationTypeName = _locationTypes.FirstOrDefault(a => a.Id == find.LocationTypeId)?.Name
                }
            };
        }

        public async Task<ResultShow<IEnumerable<LocationType>>> GetAllLocationType()
        {
            return new ResultShow<IEnumerable<LocationType>>()
            {
                Status = true,
                DataValueObject = await _locationTypes.ToListAsync()
            };
        }

        public async Task<ResultShow<IEnumerable<LocationViewModel>>> GetAllLocation()
        {
            return new ResultShow<IEnumerable<LocationViewModel>>()
            {
                Status = true,
                DataValueObject = await
                    (from loc in _location
                        join locationType in _locationTypes on loc.LocationTypeId equals locationType.Id
                        where loc.Lat != null && loc.Lng != null
                        select new LocationViewModel
                        {
                            Id = loc.Id,
                            FileName = loc.FileName,
                            LocationTypeId = loc.LocationTypeId,
                            LocationTypeName = locationType.Name,
                            Lat = loc.Lat,
                            Lng = loc.Lng,
                            LocationName = loc.LocationName
                        }
                    ).ToListAsync()
            };
        }

        public async Task<ResultShow<LocationViewModel>> SubmitLocation(LocationViewModel model)
        {
            try
            {
                var find = await _location.FirstOrDefaultAsync(x => x.Id == model.Id);

                // edit
                if (find != null)
                {
                    find.LocationName = model.LocationName;
                    find.Lat = model.Lat;
                    find.Lng = model.Lng;
                    find.LocationTypeId = model.LocationTypeId;

                    if (string.IsNullOrEmpty(model.FileName))
                    {
                        find.FileName = null;
                    }
                    else
                    {
                        if (!model.FileName.Contains("http"))
                        {
                            find.FileName = model.FileName;
                        }
                    }


                    _location.Update(find);
                    await _uow.SaveChangesAsync();

                    var select = await SelectLocation(find.Id);
                    return new ResultShow<LocationViewModel>()
                    {
                        Status = true,
                        DataValueObject = select.DataValueObject
                    };
                }

                // new
                var newItem = new Location()
                {
                    LocationName = model.LocationName,
                    Lat = model.Lat,
                    Lng = model.Lng,
                    LocationTypeId = model.LocationTypeId,
                    FileName = model.FileName,
                };
                await _location.AddAsync(newItem);
                await _uow.SaveChangesAsync();

                var select2 = await SelectLocation(newItem.Id);

                return new ResultShow<LocationViewModel>()
                {
                    Status = true,
                    DataValueObject = select2.DataValueObject
                };
            }
            catch (Exception e)
            {
                return new ResultShow<LocationViewModel>()
                {
                    Status = false,
                    DataValue = ErrorHandling.edit.ToString(),
                    Message = e.Message
                };
            }
        }
    }
}