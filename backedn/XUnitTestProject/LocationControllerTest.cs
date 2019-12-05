using LocationMap.DomainClasses.ViewModels;
using LocationMap.Services;
using LocationMap.WebApp.Controllers;
using Microsoft.AspNetCore.Hosting;
using Moq;
using Xunit;

namespace XUnitTestProject
{
    // LocationController loc 
    public class LocationControllerTest
    {
        private readonly LocationController _locationController;
        private readonly ILocationService _locationServiceFake;

        public LocationControllerTest()
        {
            //Arrange
            var mockEnvironment = new Mock<IHostingEnvironment>();
            //...Setup the mock as needed
            mockEnvironment
                .Setup(m => m.EnvironmentName)
                .Returns("Hosting:UnitTestEnvironment");
            //...other setup for mocked IHostingEnvironment...


            _locationServiceFake = new LocationServiceFake();
            _locationController = new LocationController(_locationServiceFake, mockEnvironment.Object);
        }

        /* [Fact]
         public void SelectLocation_WhenCalled_ReturnsOkResult()
         {
             // Act
             var res = _locationController.SelectLocation(2).Result;
 
             // Assert
             Assert.NotNull(res.DataValueObject);
         }*/


        [Fact]
        public void GetAllLocationType_WhenCalled_ReturnsOkResult()
        {
            // Act
            var res = _locationController.GetAllLocationType().Result;

            // Assert
            Assert.NotNull(res.DataValueObject);
        }


        [Fact]
        public void GetAllLocation_WhenCalled_ReturnsOkResult()
        {
            // Act
            var res = _locationController.GetAllLocation().Result;

            // Assert
            Assert.NotNull(res.DataValueObject);
        }


        /*[Fact]
        public void SubmitLocation_WhenCalled_ReturnsOkResult()
        {
            var item = new LocationViewModel()
            {
                Id = 30,
                LocationTypeId = 2,
                LocationName = "loc 30",
                Lat = "32.79651010951662",
                Lng = "51.49874913381392",
                FileName = "as.jepg"
            };
            
            // Act
            var res = _locationController.SubmitLocation(item).Result;

            // Assert
            Assert.NotNull(res.DataValueObject);
        }*/
    }
}