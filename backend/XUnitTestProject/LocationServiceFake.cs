using LocationMap.DomainClasses;
using LocationMap.DomainClasses.ViewModels;
using LocationMap.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUnitTestProject
{
    public class LocationServiceFake : ILocationService
    {
        private  List<LocationViewModel> _locations = new List<LocationViewModel>();

        private  List<LocationType> _locationTypes = new List<LocationType>();


        public LocationServiceFake()
        {
           // _locations = new List<LocationViewModel>();
          //  _locationTypes = new List<LocationType>();

            for (int i = 0; i < 20; i++)
            {
                var item = new LocationViewModel()
                {
                    Id = i + 1,
                    LocationTypeId = i + 1,
                    LocationName = "Loc " + i.ToString(),
                    Lat = "32.7965101095166" + (i + 1).ToString(),
                    Lng = "51.4987491338139" + (i + 1).ToString(),
                    FileName = $"pic{i+1}.jpeg"
                };

                _locations.Add(item);

                _locationTypes.Add(new LocationType()
                {
                    Id = i + 1,
                    Name = "type " + i + 1
                });
            }
        }

        public async Task<ResultShow<IEnumerable<LocationViewModel>>> GetAllLocation()
        {
            var res = new ResultShow<IEnumerable<LocationViewModel>>()
            {
                Status = true,
                DataValueObject = _locations.ToList()
            };

            return await Task.FromResult(res);
        }

        public async Task<ResultShow<IEnumerable<LocationType>>> GetAllLocationType()
        {
            return await Task.FromResult(new ResultShow<IEnumerable<LocationType>>()
            {
                Status = true,
                DataValueObject = _locationTypes.ToList()
            });
        }

        public async Task<ResultShow<LocationViewModel>> SelectLocation(int? id)
        {
            return await Task.FromResult(new ResultShow<LocationViewModel>()
            {
                Status = true,
                DataValueObject = _locations.FirstOrDefault(x => x.Id == id)
            });
        }

        public async Task<ResultShow<LocationViewModel>> SubmitLocation(LocationViewModel model)
        {
            var find = _locations.FirstOrDefault(x => x.Id == model.Id);
            if (find != null)
            {
                find.FileName = model.FileName;
                find.LocationTypeId = model.LocationTypeId;
                find.Lat = model.Lat;
                find.Lng = model.Lng;
                find.LocationName = model.LocationName;
            }

            _locations.Add(model);
            return await Task.FromResult(new ResultShow<LocationViewModel>()
            {
                Status = true,
                DataValueObject = find
            });
        }
    }
}