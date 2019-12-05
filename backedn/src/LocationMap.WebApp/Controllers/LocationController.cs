using LocationMap.DomainClasses;
using LocationMap.DomainClasses.ViewModels;
using LocationMap.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace LocationMap.WebApp.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    [AllowAnonymous]
    [IgnoreAntiforgeryToken]
    public class LocationController : Controller
    {
        private readonly ILocationService _locationService;

        private readonly IHostingEnvironment _environment;

        public LocationController(
            ILocationService locationService,
            IHostingEnvironment environment)
        {
            _locationService = locationService;
            _environment = environment;
        }

        [HttpGet("[action]")]
        public async Task<ResultShow<LocationViewModel>> SelectLocation(int? id)
        {
            var res = await _locationService.SelectLocation(id);
            if (!string.IsNullOrEmpty(res.DataValueObject.FileName))
            {
                res.DataValueObject.FileName =
                    $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/uploads/{res.DataValueObject.FileName}";
            }

            return res;
        }


        [HttpGet("[action]")]
        public async Task<ResultShow<IEnumerable<LocationType>>> GetAllLocationType() =>
            await _locationService.GetAllLocationType();


        [HttpGet("[action]")]
        public async Task<ResultShow<IEnumerable<LocationViewModel>>> GetAllLocation() =>
            await _locationService.GetAllLocation();


        [HttpPost("[action]")]
        public async Task<ResultShow<LocationViewModel>> SubmitLocation(LocationViewModel model)
        {
            var uploadsRootFolder = Path.Combine(_environment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsRootFolder))
            {
                Directory.CreateDirectory(uploadsRootFolder);
            }

            var files = Request.Form.Files;
            foreach (var file in files)
            {
                //TODO: do security checks ...!

                if (file == null || file.Length == 0)
                {
                    continue;
                }

                var filePath = Path.Combine(uploadsRootFolder, file.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream).ConfigureAwait(false);
                    model.FileName = file.FileName;
                }
            }

            // save data
            var result = await _locationService.SubmitLocation(model);
            return result;
        }
    }
}